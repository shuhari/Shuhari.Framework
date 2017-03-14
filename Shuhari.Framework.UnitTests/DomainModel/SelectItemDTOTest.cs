using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class SelectItemDtoTest
    {
        [Test]
        public void Ctor_ShouldSetProperties()
        {
            var dto = new SelectItemDto<int>(1, "name", true);

            Assert.AreEqual(1, dto.Id);
            Assert.AreEqual("name", dto.Name);
            Assert.IsTrue(dto.Selected);
        }
    }
}
