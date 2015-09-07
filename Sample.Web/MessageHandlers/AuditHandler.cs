using System;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using log4net;
using Sample.Domain.Model.System;
using Sample.Infrastructure.Interfaces;

namespace Sample.Web.MessageHandlers
{
    public class AuditHandler : DelegatingHandler
    {
        private IRepository<HttpAudit> auditRepo;
        private IUnitOfWork unitOfWork;

        private ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public AuditHandler()
        {
            // DelegatingHandlers are only initialized once in Web API (at application start), so we will use
            // the MVC dependency resolver registered in the global to pull in the necessary impl's
            this.auditRepo = System.Web.Mvc.DependencyResolver.Current.GetService<IRepository<HttpAudit>>();
            this.unitOfWork = System.Web.Mvc.DependencyResolver.Current.GetService<IUnitOfWork>();
        }
        
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // read request content and make sure it doesn't disappear after consuming the string
            var contentType = request.Content.Headers.ContentType;
            var contentInString = request.Content.ReadAsStringAsync().Result;
            request.Content = new StringContent(contentInString);
            request.Content.Headers.ContentType = contentType;

            var response = await base.SendAsync(request, cancellationToken);

            var httpAudit = new HttpAudit(request, contentInString, response);

            try
            {
                auditRepo.Insert(httpAudit);
                
                await unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                log.Error("Exception occurred when inserting audit message to repo", e); 
                
                // swallow the exception, or throw it depending on your needs/requirements

                throw;
            }

            // should this be above the inserts?
            await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}