using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class FileItemTest
    {
        [Test]
        public void Equals_PropertiesSame_ShouldReturnTrue()
        {
            var f1 = new FileItem("fname", "mime", new byte[] { 0x1, 0x2 });
            var f2 = new FileItem("fname", "mime", new byte[] { 0x1, 0x2 });

            Assert.AreEqual(f1, f2);
            Assert.IsTrue(f1 == f2);
            Assert.AreEqual(f1.GetHashCode(), f2.GetHashCode());
        }

        [Test]
        public void Equals_PropertyDifferent_ShouldReturnFalse()
        {
            var f1 = new FileItem("f1", "mime", new byte[] { 0x1, 0x2 });
            var f2 = new FileItem("f2", "mime", new byte[] { 0x1, 0x2 });

            Assert.AreNotEqual(f1, f2);
            Assert.IsFalse(f1 == f2);
            Assert.IsTrue(f1 != f2);
        }

        [Test]
        public void Equals_DifferentType_ShouldReturnFalse()
        {
            Assert.IsFalse(new FileItem("f1", "mime", new byte[] { 0x1 }).Equals("abc"));
        }
    }
}
