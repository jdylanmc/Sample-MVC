using System.Collections.Generic;
using System.Linq;
using Sample.Domain.Model.System;

namespace Sample.Domain.Interfaces.System
{
    public interface ILogViewer
    {
        IEnumerable<Log> GetLogs();
    }
}