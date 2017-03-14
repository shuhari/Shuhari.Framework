using System.Linq;
using NUnit.Framework;
using Shuhari.Framework.Security;

namespace Shuhari.Framework.UnitTests.Security
{
    [TestFixture]
    public class BasePasswordBuilderTest
    {
        class TestPasswordBuilder : BasePasswordBuilder
        {
            protected override byte[] ComputeHash(string password, byte[] salt)
            {
                return Encoding.GetBytes(password)
                    .Concat(salt)
                    .ToArray();
            }

            protected override byte[] GenerateSalt()
            {
                return new byte[] { 0x1, 0x2, 0x3, 0x4 };
            }
        }

        [SetUp]
        public void SetUp()
        {
            _builder = new TestPasswordBuilder();
        }

        private TestPasswordBuilder _builder;

        [Test]
        public void Generate_IsMatch()
        {
            var pair = _builder.Generate("1234");

            Assert.IsTrue(_builder.IsCorrect("1234", pair));
            Assert.IsFalse(_builder.IsCorrect("notpwd", pair));
        }
    }
}
