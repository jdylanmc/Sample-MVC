using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.Domain.Interfaces;

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
            var logs = logViewer.GetLogs();

            return View(logs);
        }
    }
}