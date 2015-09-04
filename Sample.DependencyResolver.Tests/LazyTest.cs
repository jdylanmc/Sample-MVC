using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Sample.DependencyResolver.Tests
{
    public interface ITestClass
    {
        int HighFive();
    }

    public class TestClass : ITestClass
    {
        public static int InstanceCount = 0;

        public TestClass()
        {
            Interlocked.Increment(ref InstanceCount);
        }

        public int HighFive()
        {
            return 5;
        }
    }

    [TestFixture]
    public class TestFixture
    {
        [Test]
        public void Test()
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<ITestClass, TestClass>();
                container.AddNewExtension<LazyExtension>();

                var testClass1 = container.Resolve<Lazy<ITestClass>>();

                Assert.AreEqual(false, testClass1.IsValueCreated);
                Assert.AreEqual(0, TestClass.InstanceCount);

                Assert.AreEqual(5, testClass1.Value.HighFive());
                Assert.AreEqual(true, testClass1.IsValueCreated);
                Assert.AreEqual(1, TestClass.InstanceCount);

                var testClass2 = container.Resolve<Lazy<ITestClass>>();

                Assert.AreEqual(false, testClass2.IsValueCreated);
                Assert.AreEqual(1, TestClass.InstanceCount);

                Assert.AreEqual(5, testClass2.Value.HighFive());
                Assert.AreEqual(true, testClass2.IsValueCreated);
                Assert.AreEqual(2, TestClass.InstanceCount);
            }
        }
    }
}
