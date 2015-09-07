using System;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Sample.Domain.Interfaces;
using Sample.Domain.Interfaces.Business;
using Sample.Domain.Interfaces.System;
using Sample.Domain.Logic.Business;
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
        public static IUnityContainer Initialize()
        {
            var container = BuildUnityContainer();
            System.Web.Mvc.DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // This manager allows threads to access injected resources after the page life cycle has ended
            // incredibly useful if you're doing parallel programming.  It also makes your UnitOfWork be a single
            // object in memory, which is helpful for orchestrating data manipulations before a commit
            container.RegisterType<EntityFrameworkContext>(new PerResolveLifetimeManager());
            container.RegisterType<IUnitOfWork, EntityFrameworkUnitOfWork>();
            container.RegisterType(typeof(IRepository<>), typeof(EntityFrameworkRepository<>)); 

            // register all interfaces and their implementations here
            container.RegisterType<IConfiguration, Configuration>();
            container.RegisterType<ILogViewer, LogViewer>();

            container.RegisterType<ITodoService, TodoService>();
            

            // set up unity so that it lazily loads dependencies.  helps performance of web applications
            container.AddNewExtension<LazyExtension>();

            return container;
        }
    }
}
