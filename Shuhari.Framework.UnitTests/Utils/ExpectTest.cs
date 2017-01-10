using System;
using System.IO;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class ExpectTest
    {
        private const string PARAM_NAME = "param";

        [Test]
        public void IsNotNull_ParamNull_ShouldThrow()
        {
            Assert.Throws<ExpectionException>(() => Expect.IsNotNull(null, PARAM_NAME));
        }

        [Test]
        public void IsNotNull_ParamNotNull_ShouldPass()
        {
            Expect.IsNotNull("abc", PARAM_NAME);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void IsNotBlank_ParamIsBlank_ShouldThrow(string param)
        {
            var exp = Assert.Throws<ExpectionException>(() => Expect.IsNotBlank(param, PARAM_NAME));
            StringAssert.Contains(PARAM_NAME, exp.Message);
        }

        [Test]
        public void IsNotBlankl_ParamNotBlank_ShouldPass()
        {
            Expect.IsNotBlank("abc", PARAM_NAME);
        }

        [Test]
        public void That_ResultFalse_ShouldThrow()
        {
            Assert.Throws<ExpectionException>(() => Expect.That(false, "error message"));
        }

        [Test]
        public void That_ResultTrue_ShouldPass()
        {
            Expect.That(1 > 0, "1 > 0");
        }

        [Test]
        public void That_Custom_ResultFalse_ShouldThrow()
        {
            Assert.Throws<ApplicationException>(() =>
                Expect.That(false, () => new ApplicationException("test error")));
        }

        [Test]
        public void That_Custom_ResultTrue_ShouldPass()
        {
            Expect.That(true, () => new ApplicationException("error"));
        }

        [Test]
        public void FileExist_ParamNotExist_ShouldThrow()
        {
            Assert.Throws<ExpectionException>(() => Expect.FileExist("file_not_exist"));
        }

        [Test]
        public void FileExist_ParamExist_ShouldPass()
        {
            Expect.FileExist(GetType().Assembly.Location);
        }

        [Test]
        public void DirectoryExist_ParamNotExist_ShouldThrow()
        {
            Assert.Throws<ExpectionException>(() => Expect.DirectoryExist("dir_not_exist"));
        }

        [Test]
        public void DirectoryExist_ParamExist_ShouldPass()
        {
            Expect.DirectoryExist(Path.GetDirectoryName(GetType().Assembly.Location));
        }
    }
}
