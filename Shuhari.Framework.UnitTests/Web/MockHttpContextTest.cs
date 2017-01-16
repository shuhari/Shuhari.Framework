using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Shuhari.Framework.UnitTests.Web.Mvc;
using Shuhari.Framework.Web;

namespace Shuhari.Framework.UnitTests.Web
{
    [TestFixture]
    public class MockHttpContextTest
    {
        [SetUp]
        public void SetUp()
        {
            _ctx = new MockHttpContext();
        }

        private MockHttpContext _ctx;

        [Test]
        public void Session_ShouldBeNotNull()
        {
            Assert.IsNotNull(_ctx.Session);
        }

        [Test]
        public void User_DefaultShouldBeUnauthenticated()
        {
            Assert.IsNotNull(_ctx.User);
            Assert.IsNotNull(_ctx.User.Identity);
            Assert.IsFalse(_ctx.User.Identity.IsAuthenticated);
        }

        [Test]
        public void CreateControllerContext_ContextShouldBeValid()
        {
            var controller = new TestController();
            var controllerCtx = _ctx.CreateControllerContext(controller);

            Assert.IsNotNull(controllerCtx);
            Assert.AreSame(_ctx, controllerCtx.HttpContext);
            Assert.AreSame(controller, controllerCtx.Controller);
        }

        [Test]
        public void CreateViewContext_ContextShouldBeValid()
        {
            var viewMock = new Mock<IView>();
            var controller = new TestController();

            var viewCtx = _ctx.CreateViewContext(controller, viewMock.Object);
            Assert.IsNotNull(viewCtx);
            Assert.AreSame(controller, viewCtx.Controller);
            Assert.AreSame(_ctx, viewCtx.HttpContext);
        }
    }
}
