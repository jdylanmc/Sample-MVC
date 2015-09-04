using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Domain.Interfaces;
using Sample.Domain.Model.System;

namespace Sample.Domain.Logic.System
{
    public class LogViewer : ILogViewer
    {
        public IEnumerable<Log> GetLogs()
        {
            // this is where you would assert that the user has permissions to view logs

            return new List<Log>()
            {
                new Log
                {
                    Date = DateTime.Now.AddDays(-1),
                    Exception = "test",
                    Level = "level",
                    Logger = "logger1",
                    Message = "msg",
                    Thread = "8"
                },
                new Log
                {
                    Date = DateTime.Now,
                    Exception = "other",
                    Level = "level2",
                    Logger = "logger2",
                    Message = "msg2",
                    Thread = "9"
                }
            };
        }
    }
}
