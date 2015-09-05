using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Domain.Model.System;

namespace Sample.Infrastructure.Interfaces.Repositories
{
    public interface ILogRepository
    {
        IQueryable<Log> Select();
    }
}
