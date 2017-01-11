using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Globalization
{
    [TestFixture]
    public class FrameworkStringsTest
    {
        [Test]
        public void Strings_ShouldDefine()
        {
            foreach (var field in typeof(FrameworkStrings).GetFields(BindingFlags.NonPublic | BindingFlags.Static)
                .Where(x => x.GetCustomAttribute<DisplayAttribute>() != null))
            {
                var value = (string)field.GetValue(null);
                Assert.That(value.IsNotBlank(), string.Format("Framework string {0} not found in resource", field.Name));
            }
        }
    }
}
