using System.Web.Mvc;
using NUnit.Framework;
using Shuhari.Framework.Web.Mvc;

namespace Shuhari.Framework.UnitTests.Web.Mvc
{
    [TestFixture]
    public class TagExtensionTest
    {
        [Test]
        public void AddClass_Html()
        {
            Assert.AreEqual("<i class=\"c1\"></i>", new TagBuilder("i").AddClass("c1").ToHtml().ToHtmlString());
        }

        [Test]
        public void Html()
        {
            Assert.AreEqual("<i>html</i>", new TagBuilder("i").Html("html").ToHtml().ToHtmlString());
        }

        [Test]
        public void AppendHtml()
        {
            Assert.AreEqual("<i>h1h2</i>", new TagBuilder("i").AppendHtml("h1").AppendHtml("h2").ToHtml().ToHtmlString());
        }

        [Test]
        public void Attr()
        {
            Assert.AreEqual("<i id=\"i1\"></i>", new TagBuilder("i").Attr("id", "i1").ToHtml().ToHtmlString());
        }
    }
}
