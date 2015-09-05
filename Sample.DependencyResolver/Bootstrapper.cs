using System;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Sample.Domain.Interfaces;
using Sample.Domain.Interfaces.System;
using Sample.Domain.Logic.System;
using Unity.Mvc5;
using Sample.Infrastructure.Configuration;
using Sample.Infrastructure.Data;
using Sample.Infrastructure.Interfaces;
using Sample.Infrastructure.Interfaces.Repositories;
using Sample.Infrastructure.Repository;
using Sample.Infrastructure.Repository.Repositories;

namespace Sample.DependencyResolver
{
    public static class Bootstrapper
    {
        // if you have a standalone API project, you may need to call in like this
        public static IUnityContainer InitializeApi()
        {
            var container = BuildUnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        // if you have a standard mvc project, this seems to work fine
        public static IUnityContainer InitializeWeb()
        {
            var container = BuildUnityContainer();
            System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // This manager allows threads to access injected resources after the page life cycle has ended
            // incredibly useful if you're doing parallel programming
            container.RegisterType<EntityContext>(new PerResolveLifetimeManager());

            // register all interfaces and their implementations here
            container.RegisterType<IConfiguration, Configuration>();
            container.RegisterType<ILogViewer, LogViewer>();

            container.RegisterType<IHttpAuditRepository, HttpAuditRepository>();
            container.RegisterType<ILogRepository, LogRepository>();

            // set up unity so that it lazily loads dependencies.  helps performance of web applications
            container.AddNewExtension<LazyExtension>();

            return container;
        }
    }
}
