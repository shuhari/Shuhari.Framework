using System;
using System.Text;
using NUnit.Framework;
using Shuhari.Framework.Security;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Security
{
    [TestFixture]
    public class EncryptionTest
    {
        [TestCase("1234", "81DC9BDB52D04DC20036DBD8313ED055")]
        public void MD5(string str, string result)
        {
            AssertEncryption(Encryption.MD5, str, result);
        }

        [TestCase("1234", "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220")]
        public void SHA1(string str, string result)
        {
            AssertEncryption(Encryption.SHA1, str, result);
        }

        [TestCase("1234", "03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4")]
        public void SHA256(string str, string result)
        {
            AssertEncryption(Encryption.SHA256, str, result);
        }

        private void AssertEncryption(Func<byte[], byte[]> fn, string input, string result)
        {
            var data = Encoding.UTF8.GetBytes(input);
            Assert.AreEqual(result, fn(data).ToHex(true));
        }
    }
}
