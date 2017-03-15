using System.Web.Mvc;
using NUnit.Framework;
using Shuhari.Framework.Web.Mvc;

namespace Shuhari.Framework.UnitTests.Web.Mvc
{
    [TestFixture]
    public class MvcExtensionsTest
    {
        [Test]
        public void ModelState_NoError()
        {
            var state = new ModelStateDictionary();

            Assert.IsNull(state.GetFirstError());
            CollectionAssert.IsEmpty(state.GetErrors());
        }

        [Test]
        public void ModelState_HasError()
        {
            var state = new ModelStateDictionary();
            state.AddModelError("", "test error");

            Assert.IsNotNull(state.GetFirstError());
            Assert.AreEqual(1, state.GetErrors().Length);
        }
    }
}