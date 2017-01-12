using System.IO;
using System.Text;
using NUnit.Framework;
using Shuhari.Framework.Text;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Text
{
    [TestFixture]
    public class StringReplacerTest
    {
        [Test]
        public void Apply()
        {
            Assert.AreEqual("xyz", new StringReplacer("abcd", "xyz").Apply("abcd"));
        }

        [Test]
        public void ApplyToFile()
        {
            var filePath = GetType().Assembly.GetResource("ResourceFiles/res.txt").CopyToBaseDirectory();
            var replacer = new StringReplacer("abcd", "xyz");
            replacer.ApplyToFile(filePath, Encoding.UTF8);

            Assert.AreEqual("xyz", File.ReadAllText(filePath, Encoding.UTF8));
        }
    }
}
