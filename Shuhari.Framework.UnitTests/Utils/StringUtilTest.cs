using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class StringUtilTest
    {
        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase(" ", true)]
        [TestCase("abc", false)]
        [TestCase(" abc ", false)]
        public void IsBlank_IsNotBlank(string param, bool isBlank)
        {
            Assert.AreEqual(isBlank, param.IsBlank());
            Assert.AreEqual(!isBlank, param.IsNotBlank());
        }

        [TestCase(null, null, true)]
        [TestCase(null, "", false)]
        [TestCase("", null, false)]
        [TestCase("", "", true)]
        [TestCase("abc", "abc", true)]
        [TestCase("abc", "ABC", true)]
        [TestCase("ABC", "abc", true)]
        [TestCase("abc", "abcd", false)]
        public void EqualsNoCase(string lhs, string rhs, bool result)
        {
            Assert.AreEqual(result, lhs.EqualsNoCase(rhs));
        }

        [TestCase(null, null, true)]
        [TestCase(null, "", false)]
        [TestCase("", null, true)]
        [TestCase("", "", true)]
        [TestCase("abc", "ab", true)]
        [TestCase("abc", "bc", true)]
        [TestCase("abc", "BC", true)]
        [TestCase("abc", "bcd", false)]
        public void ContainsNoCase(string lhs, string rhs, bool result)
        {
            Assert.AreEqual(result, lhs.ContainsNoCase(rhs));
        }
    }
}