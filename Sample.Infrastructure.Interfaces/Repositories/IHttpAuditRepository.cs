using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Sample.Domain.Model.System;

namespace Sample.Infrastructure.Interfaces.Repositories
{
    public interface IHttpAuditRepository
    {
        Task Insert(HttpAudit httpAudit);

        IQueryable<HttpAudit> Select();
    }
}
