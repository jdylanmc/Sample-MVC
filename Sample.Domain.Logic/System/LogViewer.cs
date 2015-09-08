using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Repository;
using Sample.Domain.Interfaces;
using Sample.Domain.Interfaces.System;
using Sample.Domain.Model.System;
using Sample.Infrastructure.Repository;

namespace Sample.Domain.Logic.System
{
    public class LogViewer : ILogViewer
    {
        private EntityFrameworkContext context;

        public LogViewer(EntityFrameworkContext context)
        {
            this.context = context;
        }

        public IEnumerable<Log> GetLogs()
        {
            // this is where you would assert that the user has permissions to view logs

            return context.Log.OrderByDescending(x => x.Date).Take(20);
        }
    }
}
