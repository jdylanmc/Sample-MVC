using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Sample.Domain.Model.System;
using Sample.Infrastructure.Data;
using Sample.Infrastructure.Interfaces.Repositories;

namespace Sample.Infrastructure.Repository.Repositories
{
    public class HttpAuditRepository : AbstractRepository, IHttpAuditRepository
    {
        public HttpAuditRepository(EntityContext context)
            : base(context) { }
        
        private ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task Insert(HttpAudit httpAudit)
        {
            try
            {
                context.HttpAudit.Add(httpAudit);

                await context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                log.Error("Error saving HttpAudit message. " + FormatDbEntityValidationException(e), e);

                throw;
            }
        }

        public IQueryable<HttpAudit> Select()
        {
            throw new System.NotImplementedException();
        }
    }
}