using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    /// <summary>
    /// Generates an error to test logging/500 handling
    /// </summary>
    public class ErrorController : Controller
    {
        // GET: ExceptionTester
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }
    }
}