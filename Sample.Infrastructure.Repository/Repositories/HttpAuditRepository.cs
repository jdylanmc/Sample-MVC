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
        public HttpAuditRepository(EntityFrameworkContext entityContext)
            : base(entityContext) { }
        
        private ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task Insert(HttpAudit httpAudit)
        {
            try
            {
                entityContext.HttpAudit.Add(httpAudit);

                await entityContext.SaveChangesAsync();
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