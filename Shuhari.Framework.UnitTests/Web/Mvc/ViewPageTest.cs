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

        [Test]
        public void ToSelectItem()
        {
            var dto = new SelectItemDto<int>(12, "n", true);
            var item = _page.ToSelectItem(dto);
            AssertSelectItem(item, "n", "12", true);
        }

        [Test]
        public void ToSelectItems()
        {
            var dto = new SelectItemDto<int>(12, "n", true);
            var items = _page.ToSelectItems(new[] { dto });

            Assert.AreEqual(1, items.Length);
            AssertSelectItem(items[0], "n", "12", true);
        }

        private void AssertSelectItem(SelectListItem item, string text, string value, bool selected)
        {
            Assert.AreEqual(text, item.Text);
            Assert.AreEqual(value, item.Value);
            Assert.AreEqual(selected, item.Selected);
        }

        [Test]
        public void TempMessage()
        {
            const string MSG = "test msg";
            var setup = new UserManagerSetup();
            var mockView = new Mock<IView>();
            _page.ViewContext = setup.HttpContext.CreateViewContext(new TestController(), mockView.Object);

            _page.TempMessage = MSG;
            Assert.AreEqual(MSG, _page.TempMessage);
        }
    }
}
