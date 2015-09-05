using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Domain.Model.System;
using Sample.Infrastructure.Interfaces.Repositories;

namespace Sample.Infrastructure.Repository.Repositories
{
    public class LogRepository : AbstractRepository, ILogRepository
    {
        public LogRepository(EntityContext context)
            : base(context) { }

        // be mindful that IQueryable's can be modified and will persist
        public IQueryable<Log> Select()
        {
            return base.context.Log;
        }
    }
}
