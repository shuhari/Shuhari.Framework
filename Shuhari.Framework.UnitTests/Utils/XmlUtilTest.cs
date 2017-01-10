using System.Xml;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class XmlUtilTest
    {
        [SetUp]
        public void SetUp()
        {
            _doc = new XmlDocument();
            _doc.LoadXml(@"<root attr1='value1'></root>");
        }

        private XmlDocument _doc;

        [Test]
        public void SafeAttr_Exist_ShouldReurnValue()
        {
            Assert.AreEqual("value1", _doc.DocumentElement.SafeAttr("attr1"));
        }

        [Test]
        public void SafeAtr_NotExist_ShouldReturnNull()
        {
            Assert.IsNull(_doc.DocumentElement.SafeAttr("noattr"));
        }

        [Test]
        public void SafeAttr_NotExist_WithDefault_ShouldReturnDefault()
        {
            Assert.AreEqual("default", _doc.DocumentElement.SafeAttr("noattr", "default"));
        }
    }
}