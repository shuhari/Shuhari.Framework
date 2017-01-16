using System.Linq;
using NUnit.Framework;
using Shuhari.Framework.Web.Mvc;

namespace Shuhari.Framework.UnitTests.Web.Mvc
{
    [TestFixture]
    public class NinjectDependencyResolverTest
    {
        public interface Intf1 { }
        public interface Intf2 { }
        public class Class1 : Intf1 { }

        [SetUp]
        public void SetUp()
        {
            _resolver = new NinjectDependencyResolver();
            _resolver.Kernel.Bind<Intf1>().To<Class1>();
        }

        private NinjectDependencyResolver _resolver;

        [Test]
        public void Kernel_ShouldBeNotNull()
        {
            Assert.IsNotNull(_resolver.Kernel);
        }

        [Test]
        public void GetService_BindingDefined_ShouldReturnDefined()
        {
            Assert.IsInstanceOf<Class1>(_resolver.GetService(typeof(Intf1)));
        }

        [Test]
        public void GetService_BindingNotDefined_ShouldReturnNull()
        {
            Assert.IsNull(_resolver.GetService(typeof(Intf2)));
        }

        [Test]
        public void GetServices_BindingDefined_ShouldReturnDefined()
        {
            var services = _resolver.GetServices(typeof(Intf1));
            Assert.AreEqual(1, services.Count());
            Assert.IsInstanceOf<Class1>(services.First());
        }
    }
}
