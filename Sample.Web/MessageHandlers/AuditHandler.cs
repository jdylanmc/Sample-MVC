using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using log4net;
using Sample.Domain.Model.System;
using Sample.Infrastructure.Interfaces.Repositories;

namespace Sample.Web.MessageHandlers
{
    public class AuditHandler : DelegatingHandler
    {
        private IHttpAuditRepository auditor;

        private ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AuditHandler()
        {
            // DelegatingHandlers are only initialized once in Web API (at application start), so we will use
            // the MVC dependency resolver registered in the global to pull in the HttpAuditRepo impl
            this.auditor = System.Web.Mvc.DependencyResolver.Current.GetService<IHttpAuditRepository>();
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

            await auditor.Insert(httpAudit);

            await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}