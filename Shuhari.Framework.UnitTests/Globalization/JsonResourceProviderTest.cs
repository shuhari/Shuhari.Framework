using System.Globalization;
using NUnit.Framework;
using Shuhari.Framework.Globalization;

namespace Shuhari.Framework.UnitTests.Globalization
{
    [TestFixture]
    public class JsonResourceProviderTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _provider = JsonResourceProvider.GetPredefined();
        }

        private JsonResourceProvider _provider;

        [TestCase("Ok", "en", "OK")]
        [TestCase("Ok", "zh-CN", "确定")]
        public void GetString_CultureDefined(string key, string cultureName, string result)
        {
            var culture = new CultureInfo(cultureName);
            Assert.AreEqual(result, _provider.GetString(key, culture));
        }

        [Test]
        public void GetString_KeyDefined_CultureNotFound_ShouldReturnFallback()
        {
            var culture = new CultureInfo("fr");
            Assert.AreEqual("OK", _provider.GetString("Ok", culture));
        }

        [Test]
        public void GetString_KeyNotExist_ShouldReturnNull()
        {
            Assert.IsNull(_provider.GetString("key_not_exist", new CultureInfo("en")));
        }

        [TestCase("Ok")]
        [TestCase("Cancel")]
        [TestCase("Signin")]
        [TestCase("Signout")]
        [TestCase("Create")]
        [TestCase("Edit")]
        [TestCase("Delete")]
        [TestCase("Submit")]
        [TestCase("Save")]
        [TestCase("Search")]
        [TestCase("Find")]
        [TestCase("Enable")]
        [TestCase("Disable")]
        [TestCase("SelectAll")]
        [TestCase("SelectNone")]
        [TestCase("SelectInvert")]
        [TestCase("Import")]
        [TestCase("Export")]
        [TestCase("Download")]
        [TestCase("Upload")]
        [TestCase("Navigation")]
        [TestCase("Sitemap")]
        [TestCase("Name")]
        [TestCase("DisplayName")]
        [TestCase("Description")]
        [TestCase("Disabled")]
        [TestCase("CreateBy")]
        [TestCase("CreateAt")]
        [TestCase("UpdateBy")]
        [TestCase("UpdateAt")]
        [TestCase("Password")]
        [TestCase("OldPassword")]
        [TestCase("NewPassword")]
        [TestCase("ConfirmPassword")]
        [TestCase("SigninModel.UserName")]
        [TestCase("SigninModel.RememberMe")]
        [TestCase("StartTime")]
        [TestCase("EndTime")]
        [TestCase("View")]
        public void ContainsKey(string key)
        {
            Assert.IsNotNull(_provider.GetString(key, new CultureInfo("en")));
        }
    }
}
