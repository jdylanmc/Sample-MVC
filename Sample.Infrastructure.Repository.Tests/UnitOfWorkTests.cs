using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Sample.Infrastructure.Interfaces;
using Sample.Infrastructure.Repository;

namespace Sample.Infrastructure.Data.Tests
{
    public class UnityInjectionTests
    {
        /// <summary>
        /// test that IUnitOfWork is being injected properly by the DI container
        /// </summary>
        [Test]
        public void TwoResolutionsOfUnitOfWorkShouldBeTheSameObject()
        {
            var target = new UnityContainer();
            target.RegisterType<IUnitOfWork, EntityFrameworkUnitOfWork>();

            //act
            var unitOfWork1 = target.Resolve<IUnitOfWork>();
            var unitOfWork2 = target.Resolve<IUnitOfWork>();

            // assert
            unitOfWork1.Should().Be(unitOfWork2);
        }         
    }
}
