using System.Linq;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class AssemblyUtilTest
    {
        [Test]
        public void GetResource_ShouldExist()
        {
            Assert.IsNotNull(GetType().Assembly.GetResource("ResourceFiles/res.txt"));
        }

        [TestCase("ResourceFiles/res.txt")]
        public void GetAllResources(string resourcePath)
        {
            var resources = GetType().Assembly.GetAllResources();
            Assert.IsTrue(resources.Any(x => x.ResourcePath.EqualsNoCase(resourcePath)));
        }
    }
}