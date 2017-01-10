using NUnit.Framework;
using Shuhari.Framework.Security;

namespace Shuhari.Framework.UnitTests.Security
{
    [TestFixture]
    public class CommonPasswordBuilderTest
    {
        [Test]
        public void Generate_IsMatch()
        {
            var builder = new SimplePasswordBuilder();
            var pair = builder.Generate("1234");

            Assert.IsTrue(builder.IsCorrect("1234", pair));
            Assert.IsFalse(builder.IsCorrect("notpwd", pair));
        }
    }
}
