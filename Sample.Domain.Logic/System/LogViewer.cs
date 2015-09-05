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
using Sample.Infrastructure.Interfaces.Repositories;

namespace Sample.Domain.Logic.System
{
    public class LogViewer : ILogViewer
    {
        private ILogRepository logRepository;

        public LogViewer(ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        public IQueryable<Log> GetLogs()
        {
            // this is where you would assert that the user has permissions to view logs

            return logRepository.Select();
        }
    }
}
