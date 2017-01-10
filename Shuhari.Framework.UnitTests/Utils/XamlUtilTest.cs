using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class XamlUtilTest
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Serialize_Deserialize(bool formatted)
        {
            var str = "abcd";
            var text = XamlUtil.Serialize(str, formatted);
            // Console.WriteLine(text);

            var deser = XamlUtil.Deserialize<string>(text);
            Assert.AreEqual(deser, str);
        }
    }

}