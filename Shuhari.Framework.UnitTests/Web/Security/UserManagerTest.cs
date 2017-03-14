using System.Configuration;
using Moq;
using NUnit.Framework;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Web.Security;

namespace Shuhari.Framework.UnitTests.Web.Security
{
    [TestFixture]
    public class UserManagerTest
    {
        [SetUp]
        public void SetUp()
        {
            _setup = new UserManagerSetup();
            _setup.HttpContext.SetUser("user");
            _user = new UserInfo();
        }

        private UserManagerSetup _setup;
        private UserInfo _user;

        [Test]
        public void GetInstance_ShouldReturnRegistered()
        {
            Assert.AreSame(_setup.Manager, UserManager.Instance);
        }

        [Test]
        public void GetInstance_NotRegistered_ShouldThrow()
        {
            _setup.Resolver.Kernel.Unbind<UserManager>();
            Assert.Throws<ConfigurationErrorsException>(() => { UserManager.Instance.ToString(); });
        }

        [Test]
        public void Signin_AuthReturnValid_ShouldSetUser()
        {
            _setup.AuthMock.Setup(m => m.Authenticate(It.IsAny<SigninModel>())).Returns(_user);
            _setup.Manager.Signin(_setup.HttpContext, new SigninModel("user", "pwd", false));

            Assert.IsTrue(_setup.Manager.FormsSigninCalled);
            Assert.AreEqual(_user, _setup.CurrentUser);
        }

        [Test]
        public void SignOut_ShouldRemoveUser()
        {
            _setup.Manager.SignOut(_setup.HttpContext);

            Assert.IsTrue(_setup.Manager.FormsSignOutCalled);
            Assert.IsNull(_setup.CurrentUser);
        }

        [Test]
        public void GetCurrentUser_UserNotAuthenticated_ShouldReturnNull()
        {
            _setup.HttpContext.SetUserAnonymous();

            Assert.IsNull(_setup.CurrentUser);
        }

        [Test]
        public void GetCurrentUser_AlreadyExist_ShouldReturnSame()
        {
            _setup.HttpContext.Session[UserManager.USER_KEY] = _user;

            Assert.AreEqual(_user, _setup.CurrentUser);
        }

        [Test]
        public void GetCurrentUser_NotExist_ShouldReturnAuthResult()
        {
            _setup.HttpContext.Session.Remove(UserManager.USER_KEY);
            _setup.AuthMock.Setup(m => m.GetUser(It.IsAny<string>())).Returns(_user);

            Assert.AreEqual(_user, _setup.CurrentUser);
        }
    }
}
