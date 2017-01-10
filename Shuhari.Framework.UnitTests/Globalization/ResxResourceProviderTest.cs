using System.Globalization;
using NUnit.Framework;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.UnitTests.ResourceFiles;

namespace Shuhari.Framework.UnitTests.Globalization
{
    [TestFixture]
    public class ResxResourceProviderTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _provider = new ResxResourceProvider(TestResources.ResourceManager);
        }

        private ResxResourceProvider _provider;

        [TestCase("TestName", "en", "Test Name")]
        [TestCase("TestName", "zh-CN", "测试名称")]
        public void GetString(string key, string cultureName, string result)
        {
            var culture = new CultureInfo(cultureName);
            Assert.AreEqual(result, _provider.GetString(key, culture));
        }
    }
}
