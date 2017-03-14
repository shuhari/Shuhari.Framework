using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class ResultDtoTest
    {
        [Test]
        public void Ctor_ShouldBeSuccess()
        {
            var dto = new ResultDto();

            Assert.IsTrue(dto.Success);
            Assert.IsNull(dto.Message);
        }

        [Test]
        public void SetResult_ShouldSetProperties()
        {
            var dto = new ResultDto();
            dto.SetResult(false, "error");

            Assert.IsFalse(dto.Success);
            Assert.AreEqual("error", dto.Message);
        }
    }
}
