using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Security;
using Shuhari.Framework.Web;
using Shuhari.Framework.Web.Mvc;
using Shuhari.Framework.Web.Mvc.Filters;
using Shuhari.Framework.Web.Security;

namespace Shuhari.Framework.UnitTests.Web.Mvc.Filters
{
    [TestFixture]
    public class PermissionAttributeTest
    {
        [SetUp]
        public void SetUp()
        {
            _user = new UserInfo { Permissions = new string[0] };
            _mockAuth = new Mock<IAuthentication>();
            _mockAuth.Setup(m => m.GetUser(It.IsAny<string>())).Returns(_user);
            var userManager = new UserManager(_mockAuth.Object);
            var resolver = new NinjectDependencyResolver();
            resolver.Kernel.Bind<UserManager>().ToConstant(userManager);
            DependencyResolver.SetResolver(resolver);

            _mockCtx = new MockHttpContext();
            _mockCtx.SetUser("user");
            var controller = new TestController();
            var controllerCtx = _mockCtx.CreateControllerContext(controller);
            var descriptor = new Mock<ActionDescriptor>().Object;
            _execCtx = new ActionExecutingContext(controllerCtx, descriptor, new Dictionary<string, object>());
            _filter = new PermissionAttribute("p1");
        }

        private Mock<IAuthentication> _mockAuth;
        private MockHttpContext _mockCtx;
        private ActionExecutingContext _execCtx;
        private PermissionAttribute _filter;
        private UserInfo _user;

        [Test]
        public void OnAuthorization_HasPermission_ShouldPassResult()
        {
            _user.Permissions = new[] { "p1", "p2" };

            _filter.OnActionExecuting(_execCtx);
            Assert.IsNull(_execCtx.Result);
        }

        [Test]
        public void OnAuthorization_NoPermission_ShouldReturn403()
        {
            _user.Permissions = new string[0];

            _filter.OnActionExecuting(_execCtx);
            var result = (HttpStatusCodeResult)_execCtx.Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(403, result.StatusCode);
        }
    }
}
