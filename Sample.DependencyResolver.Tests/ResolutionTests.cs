using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Sample.Domain.Model.Business;
using Sample.Infrastructure.Interfaces;
using Sample.Infrastructure.Repository;

namespace Sample.DependencyResolver.Tests
{
    public class ResolutionTests
    {
        [Test]
        public void CanResolveRepository()
        {
            var target = Bootstrapper.Initialize();

            //act
            var repository = target.Resolve<EntityFrameworkContext>();

            // assert
            repository.Should().NotBeNull();
            repository.Should().BeOfType<EntityFrameworkContext>();
        }


        [Test]
        public void CanResolveConfiguration()
        {
            var target = Bootstrapper.Initialize();

            //act
            var repository = target.Resolve<IConfiguration>();

            // assert
            repository.Should().NotBeNull();
            repository.Should().BeOfType<Sample.Infrastructure.Configuration.Configuration>();
        }

        [Test]
        public void TwoResolutionsOfEFShouldBeTheSameObject()
        {
            var target = Bootstrapper.Initialize();

            //act
            var unitOfWork1 = target.Resolve<EntityFrameworkContext>();
            var unitOfWork2 = target.Resolve<EntityFrameworkContext>();

            // assert
            unitOfWork1.Should().Be(unitOfWork2);
        }    
    }
}
