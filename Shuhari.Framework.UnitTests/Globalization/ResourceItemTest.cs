using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Shuhari.Framework.Globalization;

namespace Shuhari.Framework.UnitTests.Globalization
{
    [TestFixture]
    public class ResourceItemTest
    {
        [SetUp]
        public void SetUp()
        {
            var root = (JObject)JsonConvert.DeserializeObject("{\"1\":{\"en\":\"en\",\"zh-CN\":\"cn\"}}");
            _item = new ResourceItem();
            _item.Load(root.Property("1"));
        }

        private ResourceItem _item;

        [TestCase("en", null, "en")]
        [TestCase("zh-CN", null, "cn")]
        [TestCase("zh-CN", "en", "cn")]
        [TestCase("fr", "en", "en")]
        [TestCase("fr", "au", null)]
        public void GetString(string cultureName, string fallbackName, string result)
        {
            Assert.AreEqual(result, _item.GetString(cultureName, fallbackName));
        }
    }
}
