using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace Sample.DependencyResolver
{
    /// <summary>
    /// Ripped from from Tom DuPont .NET: http://www.tomdupont.net/2012_07_01_archive.html
    /// 
    /// Essentially, this will trigger unity to only load dependencies on an as neeed basis
    /// </summary>
    public class LazyExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Policies.Set<IBuildPlanPolicy>(new LazyBuildPlanPolicy(), typeof (Lazy<>));
        }

        public class LazyBuildPlanPolicy : IBuildPlanPolicy
        {
            public void BuildUp(IBuilderContext context)
            {
                if (context.Existing != null)
                {
                    return;
                }

                var container = context.NewBuildUp<IUnityContainer>();
                var typeToBuild = context.BuildKey.Type.GetGenericArguments()[0];
                var nameToBuild = context.BuildKey.Name;
                var lazyType = typeof (Lazy<>).MakeGenericType(typeToBuild);

                var func = GetType()
                    .GetMethod("CreateResolver")
                    .MakeGenericMethod(typeToBuild)
                    .Invoke(this, new object[] {container, nameToBuild});

                context.Existing = Activator.CreateInstance(lazyType, func);

                DynamicMethodConstructorStrategy.SetPerBuildSingleton(context);
            }

            public Func<T> CreateResolver<T>(IUnityContainer currentContainer, string nameToBuild)
            {
                return () => currentContainer.Resolve<T>(nameToBuild);
            }
        }
    }
}