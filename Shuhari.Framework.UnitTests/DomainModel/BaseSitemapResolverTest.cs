using System.Linq;
using Moq;
using NUnit.Framework;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class BaseSitemapResolverTest
    {
        [SetUp]
        public void SetUp()
        {
            _user = new UserInfo { Permissions = new[] { "user", "role" } };
            _mockResolver = new Mock<BaseSitemapResolver>(_user);
            _mockResolver.Setup(m => m.ResolveUrl(It.IsAny<string>()))
                .Returns<string>(x => x.Replace("~", "@"));
            _sitemap = Fixtures.Sitemap;
            _mockResolver.Object.Resolve(_sitemap);
        }

        private UserInfo _user;
        private Mock<BaseSitemapResolver> _mockResolver;
        private Sitemap _sitemap;

        [Test]
        public void UserHasPermission_ShouldKeepNode()
        {
            var admin = FindItem(_sitemap, "Admin");
            Assert.IsNotNull(FindItem(admin, "User"));
            Assert.IsNotNull(FindItem(admin, "Role"));
        }

        [Test]
        public void UserHasNoPermission_ShouldRemove()
        {
            Assert.IsNull(FindItem(_sitemap, "NoPermParent"));
        }

        [Test]
        public void Url_ShouldResolve()
        {
            var admin = FindItem(_sitemap, "Admin");
            Assert.AreEqual("@/Admin/User", FindItem(admin, "User").Url);
        }


        private SitemapItem FindItem(SitemapContainer container, string title)
        {
            return container.Children.OfType<SitemapItem>().FindBy(x => x.Title, title, true);
        }
    }
}
