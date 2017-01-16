using NUnit.Framework;
using Shuhari.Framework.Web.Mvc;

namespace Shuhari.Framework.UnitTests.Web.Mvc
{
    [TestFixture]
    public class RedirectDataTest
    {
        [Test]
        public void Ctor_ShouldSetProperties()
        {
            var data = new RedirectData("ctrl", "action", "abc");

            Assert.AreEqual("ctrl", data.ControllerName);
            Assert.AreEqual("action", data.ActionName);
            Assert.AreEqual("abc", data.RouteValues);
        }
    }
}
