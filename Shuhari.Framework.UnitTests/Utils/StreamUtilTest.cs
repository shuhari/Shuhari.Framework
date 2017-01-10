using System.IO;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class StreamUtilTest
    {
        [Test]
        public void ReadToEnd()
        {
            var bytes = new byte[] {0x1, 0x2};
            var readed = new MemoryStream(bytes).ReadToEnd();
            CollectionAssert.AreEqual(bytes, readed);
        }
    }
}