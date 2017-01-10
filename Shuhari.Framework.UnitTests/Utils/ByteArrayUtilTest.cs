using System;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class ByteArrayUtilTest
    {
        [Test]
        public void ToHex_ParamNull_ShouldThrow()
        {
            byte[] data = null;
            Assert.Throws<ExpectionException>(() => data.ToHex());
        }

        [TestCase(false, "01ff")]
        [TestCase(true, "01FF")]
        public void ToHex(bool upperCase, string result)
        {
            var data = new byte[] { 0x01, 0xff };
            Assert.AreEqual(result, data.ToHex(upperCase));
        }

        [TestCase("01ff")]
        [TestCase("01FF")]
        [TestCase("0x01ff")]
        [TestCase("0X01FF")]
        public void Parse(string str)
        {
            var data = new byte[] { 0x01, 0xff };
            CollectionAssert.AreEqual(data, ByteArrayUtil.Parse(str));
        }

        [TestCase("1")]
        [TestCase("123")]
        public void Parse_FormatInvalid_ShouldThrow(string str)
        {
            Assert.Throws<FormatException>(() => ByteArrayUtil.Parse(str));
        }
    }
}