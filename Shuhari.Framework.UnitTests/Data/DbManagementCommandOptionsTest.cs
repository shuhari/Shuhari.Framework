using NUnit.Framework;
using Shuhari.Framework.Data;

namespace Shuhari.Framework.UnitTests.Data
{
    [TestFixture]
    public class DbManagementCommandOptionsTest
    {
        [Test]
        public void GetDefault()
        {
            var options = DbManagementCommandOptions.GetDefault();

            Assert.IsNotNull(options);
            Assert.IsNull(options.FileEncoding);
        }
    }
}
