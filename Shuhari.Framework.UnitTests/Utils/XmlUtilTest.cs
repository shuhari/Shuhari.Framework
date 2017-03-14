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
            Assert.AreEqual("value1", _doc.DocumentElement.GetAttr("attr1"));
        }

        [Test]
        public void SafeAtr_NotExist_ShouldReturnNull()
        {
            Assert.IsNull(_doc.DocumentElement.GetAttr("noattr"));
        }

        [Test]
        public void SafeAttr_NotExist_WithDefault_ShouldReturnDefault()
        {
            Assert.AreEqual("default", _doc.DocumentElement.GetAttr("noattr", "default"));
        }

        [Test]
        public void SetAttr()
        {
            _doc.DocumentElement.SetAttr("attr1", "value1")
                .SetAttr("attr2", "value2");

            Assert.AreEqual("value1", _doc.DocumentElement.GetAttr("attr1"));
            Assert.AreEqual("value2", _doc.DocumentElement.GetAttr("attr2"));
        }

        [Test]
        public void AppendElement()
        {
            _doc.DocumentElement.AppendElement("child");

            Assert.IsInstanceOf<XmlElement>(_doc.DocumentElement.SelectSingleNode("child"));
        }

        [TestCase(false, true)]
        [TestCase(true, true)]
        public void ToXmlString(bool header, bool format)
        {
            _doc = new XmlDocument();
            _doc.LoadXml(@"<root attr1='value1'></root>");
            var xml = _doc.ToXmlString(header, format);
            Assert.IsNotEmpty(xml);
            // System.Console.WriteLine(xml);
        }
    }
}