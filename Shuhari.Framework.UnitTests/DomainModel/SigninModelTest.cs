using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class SigninModelTest
    {
        [Test]
        public void Ctor_ShouldSetProperties()
        {
            var model = new SigninModel("user", "pwd", true);

            Assert.AreEqual("user", model.UserName);
            Assert.AreEqual("pwd", model.Password);
            Assert.AreEqual(true, model.RememberMe);
        }
    }
}
