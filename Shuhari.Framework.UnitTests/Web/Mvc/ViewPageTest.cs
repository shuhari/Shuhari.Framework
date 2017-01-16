using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.UnitTests.Web.Security;

namespace Shuhari.Framework.UnitTests.Web.Mvc
{
    [TestFixture]
    public class ViewPageTest
    {
        class TestViewPage : Shuhari.Framework.Web.Mvc.ViewPage<object>
        {
            public override void Execute() { }
        }

        [SetUp]
        public void SetUp()
        {
            _page = new TestViewPage();
        }

        private TestViewPage _page;

        [Test]
        public void GetPageTitle_SetPageTitle()
        {
            const string TITLE = "page title";
            _page.PageTitle = TITLE;

            Assert.AreEqual(TITLE, _page.PageTitle);
            Assert.AreEqual(TITLE, _page.ViewBag.Title);
        }

        [Test]
        public void GetCurrentUser_Authenticated_ShouldReturnUser()
        {
            var setup = new UserManagerSetup();
            setup.HttpContext.SetUser("user");
            var user = new UserInfo();
            var mockView = new Mock<IView>();
            _page.ViewContext = setup.HttpContext.CreateViewContext(new TestController(), mockView.Object);
            setup.AuthMock.Setup(m => m.GetUser(It.IsAny<string>())).Returns(user);
            setup.Manager.Signin(setup.HttpContext, new SigninModel());

            Assert.AreEqual(user, _page.CurrentUser);
        }

        [Test, SetCulture("en"), SetUICulture("en")]
        public void ResString()
        {
            Assert.AreEqual("OK", _page.ResString("Ok"));
        }
    }
}
