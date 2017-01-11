using System.IO;
using Moq;
using NUnit.Framework;
using Shuhari.Framework.IO;

namespace Shuhari.Framework.UnitTests.IO
{
    [TestFixture]
    public class StreamDecoratorTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mockStream = new Mock<Stream>();
            _decorator = new StreamDecorator(_mockStream.Object);
        }

        private Mock<Stream> _mockStream;
        private StreamDecorator _decorator;

        [TestCase(true)]
        [TestCase(false)]
        public void CanRead(bool value)
        {
            _mockStream.SetupGet(m => m.CanRead).Returns(value);

            Assert.AreEqual(value, _decorator.CanRead);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CanWrite(bool value)
        {
            _mockStream.SetupGet(m => m.CanWrite).Returns(value);

            Assert.AreEqual(value, _decorator.CanWrite);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CanSeek(bool value)
        {
            _mockStream.SetupGet(m => m.CanSeek).Returns(value);

            Assert.AreEqual(value, _decorator.CanSeek);
        }

        [Test]
        public void GetLength()
        {
            _mockStream.SetupGet(m => m.Length).Returns(123L);

            Assert.AreEqual(123L, _decorator.Length);
        }

        [Test]
        public void GetPosition()
        {
            _mockStream.SetupGet(m => m.Position).Returns(123L);

            Assert.AreEqual(123L, _decorator.Position);
        }

        [Test]
        public void SetPosition()
        {
            _decorator.Position = 123L;

            _mockStream.VerifySet(m => m.Position = 123L);
        }

        [Test]
        public void Flush()
        {
            _decorator.Flush();

            _mockStream.Verify(m => m.Flush());
        }

        [Test]
        public void Seek()
        {
            _decorator.Seek(123L, SeekOrigin.Begin);

            _mockStream.Verify(m => m.Seek(123L, SeekOrigin.Begin));
        }

        [Test]
        public void SetLength()
        {
            _decorator.SetLength(123L);

            _mockStream.Verify(m => m.SetLength(123L));
        }

        [Test]
        public void Read()
        {
            _decorator.Read(null, 0, 0);

            _mockStream.Verify(m => m.Read(null, 0, 0));
        }

        [Test]
        public void Write()
        {
            _decorator.Write(null, 0, 0);

            _mockStream.Verify(m => m.Write(null, 0, 0));
        }
    }
}
