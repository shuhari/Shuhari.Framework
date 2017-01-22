using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class SitemapTest
    {
        [SetUp]
        public void SetUp()
        {
            _sitemap = Fixtures.Sitemap;
        }

        private Sitemap _sitemap;

        [Test]
        public void FromResource_ShouldBeValid()
        {
            Assert.IsNotNull(_sitemap);
        }

        [Test]
        public void FromFile_ShouldBeValid()
        {
            var filePath = Fixtures.SitemapResource.CopyToBaseDirectory();
            var sitemap = Sitemap.FromFile(filePath);

            Assert.IsNotNull(sitemap);
        }
    }
}
