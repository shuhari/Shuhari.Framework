using System.Web.Mvc;
using Moq;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Security;
using Shuhari.Framework.Web;
using Shuhari.Framework.Web.Mvc;
using Shuhari.Framework.Web.Security;

namespace Shuhari.Framework.UnitTests.Web.Security
{
    internal class TestUserManager : UserManager
    {
        public TestUserManager(IAuthentication auth)
            : base(auth)
        {
        }

        public bool FormsSigninCalled { get; private set; }
        public bool FormsSignOutCalled { get; private set; }

        protected override void FormsSignin(SigninModel signin)
        {
            FormsSigninCalled = true;
        }

        protected override void FormsSignOut()
        {
            FormsSignOutCalled = true;
        }
    }

    /// <summary>
    /// Setup UserManager for test require HttpContext, Mock IAuth and DependencyResolver
    /// </summary>
    internal class UserManagerSetup
    {
        public UserManagerSetup()
        {
            AuthMock = new Mock<IAuthentication>();
            Manager = new TestUserManager(AuthMock.Object);
            HttpContext = new MockHttpContext();

            Resolver = new NinjectDependencyResolver();
            Resolver.Kernel.Bind<UserManager>().ToConstant(Manager);
            DependencyResolver.SetResolver(Resolver);
        }

        public Mock<IAuthentication> AuthMock { get; private set; }

        public TestUserManager Manager { get; private set; }

        public MockHttpContext HttpContext { get; private set; }

        public NinjectDependencyResolver Resolver { get; private set; }

        public UserInfo CurrentUser
        {
            get { return Manager.GetCurrentUser(HttpContext); }
        }
    }
}
