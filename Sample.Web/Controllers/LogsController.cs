using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.Domain.Interfaces;
using Sample.Domain.Interfaces.System;

namespace Sample.Web.Controllers
{
    public class LogsController : Controller
    {
        private ILogViewer logViewer;

        public LogsController(ILogViewer logViewer)
        {
            this.logViewer = logViewer;
        }

        // GET: Logs
        public ActionResult Index()
        {
            // grab the last 20 logs.  add more actions to slice data in whatever way you want.

            // also, you may want to break out a LogViewModel.

            var logs = logViewer.GetLogs().OrderByDescending(x => x.Date).Take(20);

            return View(logs);
        }
    }
}