using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.DomainModel
{
    [TestFixture]
    public class ValidationResultDTOTest
    {
        [SetUp]
        public void SetUp()
        {
            _dto = new ValidationResultDTO();
        }

        private ValidationResultDTO _dto;

        [Test]
        public void Default_IsSuccess()
        {
            Assert.IsTrue(_dto.Success);
            CollectionAssert.IsEmpty(_dto.Errors);
        }

        [Test]
        public void SetError_ShouldSetSuccessAndErrors()
        {
            const string MSG = "test msg";
            _dto.SetError("", MSG);

            Assert.IsFalse(_dto.Success);
            Assert.AreEqual(1, _dto.Errors.Length);
            Assert.AreEqual("", _dto.Errors[0].Property);
            Assert.AreEqual(MSG, _dto.Errors[0].Message);
        }
    }
}
