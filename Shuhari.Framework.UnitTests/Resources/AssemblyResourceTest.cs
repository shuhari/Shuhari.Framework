using System;
using System.IO;
using NUnit.Framework;
using Shuhari.Framework.Resources;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Resources
{
    [TestFixture]
    public class AssemblyResourceTest
    {
        [SetUp]
        public void SetUp()
        {
            _resource = GetType().Assembly.GetResource("ResourceFiles/res.txt");
        }

        private AssemblyResource _resource;

        [Test]
        public void OpenRead()
        {
            var stream = _resource.OpenRead();
            Assert.IsNotNull(stream);
            stream.Close();
        }

        [Test]
        public void OpenRead_ResourceNotExist_ShouldThrow()
        {
            Assert.Throws<ResourceException>(() =>
                GetType().Assembly.GetResource("res_not_exist").OpenRead());
        }

        [Test]
        public void ReadAllBytes()
        {
            CollectionAssert.AreEqual(EncodingUtil.DefaultEncoding.GetBytes("abcd"),
                _resource.ReadAllBytes());
        }

        [Test]
        public void ReadAllText()
        {
            Assert.AreEqual("abcd", _resource.ReadAllText(EncodingUtil.DefaultEncoding));
        }

        [Test]
        public void CopyToFile()
        {
            var destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "res.txt");
            _resource.CopyToFile(destPath);

            FileAssert.Exists(destPath);
        }

        [Test]
        public void CopyToBaseDirectory()
        {
            var destPath = _resource.CopyToBaseDirectory();

            FileAssert.Exists(destPath);
        }
    }
}