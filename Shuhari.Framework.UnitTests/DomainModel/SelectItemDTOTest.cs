using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class SelectItemDTOTest
    {
        [Test]
        public void Ctor_ShouldSetProperties()
        {
            var dto = new SelectItemDTO<int>(1, "name", true);

            Assert.AreEqual(1, dto.Id);
            Assert.AreEqual("name", dto.Name);
            Assert.IsTrue(dto.Selected);
        }
    }
}
