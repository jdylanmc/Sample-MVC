﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Sample.Domain.Model.System;

namespace Sample.Domain.Interfaces.System
{
    public interface IHttpAuditService
    {
        Task AuditHttpTraffic(HttpAudit auditMessage);
    }
}
