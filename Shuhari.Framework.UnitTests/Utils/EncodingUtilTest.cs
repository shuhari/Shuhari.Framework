using System.Text;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class EncodingUtilTest
    {
        [Test]
        public void DefaultEncoding_ShouldBeUtf8()
        {
            Assert.AreEqual(Encoding.UTF8, EncodingUtil.DefaultEncoding);
        }
    }
}