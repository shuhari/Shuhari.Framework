using System.IO;
using NUnit.Framework;
using Shuhari.Framework.IO;

namespace Shuhari.Framework.UnitTests.IO
{
    [TestFixture]
    public class FileSystemExtensionsTest
    {
        [Test]
        public void GetNewAttributes()
        {
            var attr = FileAttributes.System | FileAttributes.Hidden;
            var newAttr = FileSystemExtensions.GetNewAttributes(attr,
                FileAttributes.Archive, FileAttributes.Hidden);
            Assert.AreEqual(FileAttributes.System | FileAttributes.Archive, newAttr);
        }
    }
}