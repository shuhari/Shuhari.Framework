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

        [Test]
        public void WriteText()
        {
            var filePath = Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), "test.txt");
            new FileInfo(filePath).EnsureDirectory().WriteText("abc");

            Assert.AreEqual("abc", File.ReadAllText(filePath));
        }

        [Test]
        public void WriteBytes()
        {
            var buf = new byte[] { 0x01, 0xff };
            var filePath = Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), "test.dat");
            new FileInfo(filePath).EnsureDirectory().WriteBytes(buf);

            CollectionAssert.AreEqual(buf, File.ReadAllBytes(filePath));
        }

        [Test]
        public void ForceDelete()
        {
            var dir1 = Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), "dir1");
            var dir2 = Path.Combine(dir1, "dir2");
            Directory.CreateDirectory(dir2);

            var testFile = Path.Combine(dir2, "file.txt");
            File.WriteAllText(testFile, "abc");
            File.SetAttributes(testFile, FileAttributes.ReadOnly | FileAttributes.System);

            new DirectoryInfo(dir1).ForceDelete();
            Assert.IsFalse(Directory.Exists(dir1));
        }
    }
}