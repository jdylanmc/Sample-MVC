using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using log4net.Config;
using Sample.DependencyResolver;
using Sample.Web.MessageHandlers;

namespace Sample.Web
{
    public class WebApiApplication : HttpApplication
    {
        private ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Bootstrapper.Initialize();

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // initialize log4net
            BasicConfigurator.Configure();
            log.Info("Logging initialized");

            GlobalConfiguration.Configuration.MessageHandlers.Add(new AuditHandler());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError().GetBaseException();
            log.Error("Unhandled Application Exception", exception);
        }
    }
}
