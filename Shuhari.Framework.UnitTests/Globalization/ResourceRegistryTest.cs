using System.Globalization;
using System.Linq;
using NUnit.Framework;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.UnitTests.ResourceFiles;

namespace Shuhari.Framework.UnitTests.Globalization
{
    [TestFixture]
    public class ResourceRegistryTest
    {
        [SetUp]
        public void SetUp()
        {
            ResourceRegistry.Reset();
        }

        [Test]
        public void Clear_NotIncludeInternal_ShouldHaveInternal()
        {
            Assert.AreEqual(1, ResourceRegistry.Providers.Count());
            Assert.IsInstanceOf<JsonResourceProvider>(ResourceRegistry.Providers.First());
        }

        [Test]
        public void RegisterResx()
        {
            ResourceRegistry.RegisterResx(TestResources.ResourceManager);

            Assert.IsTrue(ResourceRegistry.Providers.Any(x => x is ResxResourceProvider));
        }

        [TestCase("Ok", "en", "OK")]
        [TestCase("Ok", "zh-CN", "确定")]
        public void GetString_Predefined_ShouldReturnDefined(string key, string cultureName, string result)
        {
            var culture = new CultureInfo(cultureName);
            Assert.AreEqual(result, ResourceRegistry.GetString(key, culture));
        }

        [SetCulture("en"), SetUICulture("en")]
        public void GetString_Predefined_CultureNotSet_ShouldUseCurrentCulture_en()
        {
            Assert.AreEqual("OK", ResourceRegistry.GetString("Ok"));
        }

        [SetCulture("zh-CN"), SetUICulture("zh-CN")]
        public void GetString_Predefined_CultureNotSet_ShouldUseCurrentCulture_zhCN()
        {
            Assert.AreEqual("确定", ResourceRegistry.GetString("Ok"));
        }

        [Test]
        public void GetString_KeyNotFound_ShouldReturnNull()
        {
            Assert.IsNull(ResourceRegistry.GetString("key_not_exist", new CultureInfo("en")));
        }

        [Test]
        public void GetUiString_KeyNotFound_ShouldReturnWithKey()
        {
            Assert.AreEqual("?key_not_exist?", ResourceRegistry.GetUiString("key_not_exist", new CultureInfo("en")));
        }
    }
}
