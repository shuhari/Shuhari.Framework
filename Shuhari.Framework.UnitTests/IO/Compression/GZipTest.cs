using System.Linq;
using NUnit.Framework;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.IO.Compression;

namespace Shuhari.Framework.UnitTests.IO.Compression
{
    [TestFixture]
    public class GZipTest
    {
        [Test]
        public void Compress_Decompress()
        {
            var f1 = new FileItem("folder/f1", "", new byte[] { 0x1, 0x2 });
            var f2 = new FileItem("folder/f2", "", new byte[] { 0xfe, 0xff });
            var zip = GZip.ComparessFiles("zip", new[] { f1, f2 });
            // System.IO.File.WriteAllBytes(@"d:/test.zip", zip.Content);
            var files = GZip.DecompressFiles(zip.Content);

            Assert.IsTrue(files.Any(x => x.Name == f1.Name && x.Content.SequenceEqual(f1.Content)));
            Assert.IsTrue(files.Any(x => x.Name == f2.Name && x.Content.SequenceEqual(f2.Content)));
        }
    }
}
