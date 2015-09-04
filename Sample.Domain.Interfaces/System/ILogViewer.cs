using System.Collections.Generic;
using Sample.Domain.Model.System;

namespace Sample.Domain.Interfaces
{
    public interface ILogViewer
    {
        IEnumerable<Log> GetLogs();
    }
}