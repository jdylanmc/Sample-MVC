using System;
using System.Threading.Tasks;
using log4net;
using Sample.Domain.Interfaces.System;
using Sample.Domain.Model.System;
using Sample.Infrastructure.Repository;

namespace Sample.Domain.Logic.System
{
    public class HttpAuditService : IHttpAuditService
    {
        private EntityFrameworkContext context;

        private ILog log = LogManager.GetLogger(typeof(HttpAuditService));

        public HttpAuditService(EntityFrameworkContext context)
        {
            this.context = context;
        }

        public async Task AuditHttpTraffic(HttpAudit auditMessage)
        {
            try
            {
                context.HttpAudit.Add(auditMessage);

                await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                log.Error("Exception occurred when inserting audit message to repo", e);

                // swallow the exception, or throw it depending on your needs/requirements

                throw;
            }
        }
    }
}