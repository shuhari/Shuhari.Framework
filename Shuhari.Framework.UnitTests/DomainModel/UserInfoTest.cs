using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class UserInfoTest
    {
        [Test]
        public void Ctor_ShouldSetProperties()
        {
            var info = new UserInfo();

            Assert.IsNotNull(info.Properties);
        }

        [TestCase("p1", true)]
        [TestCase("p99", false)]
        public void HasPermission(string permission, bool result)
        {
            var info = new UserInfo
            {
                Permissions = new[] { "p1", "p2" }
            };
            Assert.AreEqual(result, info.HasPermission(permission));
        }
    }
}
