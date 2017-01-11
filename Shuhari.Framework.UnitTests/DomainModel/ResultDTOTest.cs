using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class ResultDTOTest
    {
        [Test]
        public void Ctor_ShouldBeSuccess()
        {
            var dto = new ResultDTO();

            Assert.IsTrue(dto.Success);
            Assert.IsNull(dto.Message);
        }

        [Test]
        public void SetResult_ShouldSetProperties()
        {
            var dto = new ResultDTO();
            dto.SetResult(false, "error");

            Assert.IsFalse(dto.Success);
            Assert.AreEqual("error", dto.Message);
        }
    }
}
