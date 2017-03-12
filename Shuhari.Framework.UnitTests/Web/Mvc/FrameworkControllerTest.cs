using System;
using System.Web.Mvc;
using NUnit.Framework;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.UnitTests.Web.Mvc
{
    [TestFixture]
    public class FrameworkControllerTest
    {
        [SetUp]
        public void SetUp()
        {
            _controller = new TestController();
        }

        private TestController _controller;

        [Test]
        public void TempMessage()
        {
            const string MSG = "test msg";
            _controller.TempMessage = MSG;
            Assert.AreEqual(MSG, _controller.TempMessage);
        }

        [Test]
        public void ExecuteJsonResult_Success_ShouldReturnOk()
        {
            var result = _controller.ExecuteJsonResult("abc", x => { });
            var dto = (ResultDto)result.Data;
            Assert.IsTrue(dto.Success);
        }

        [Test]
        public void ExecuteAndRedirect_ActionExecOk_ShouldRedirect()
        {
            var result = (RedirectResult)_controller.ExecuteAndRedirect("abc", x => { }, "redirect", "msg"); 

            Assert.IsNotNull(result);
            Assert.AreEqual("redirect", result.Url);
        }

        [Test]
        public void ExecuteAndRedirect_Throws_ShouldReturnView()
        {
            var result = (ViewResult)_controller.ExecuteAndRedirect("abc", 
                x => { throw new Exception("test error"); }, 
                "redirect", "msg");

            Assert.IsNotNull(result);
            Assert.AreEqual("abc", result.Model);
        }

        [Test]
        public void ExecuteJsonResult_Throws_ShouldReturnError()
        {
            const string msg = "test error";
            var result = _controller.ExecuteJsonResult("abc", x => { throw new Exception(msg); });
            var dto = (ResultDto)result.Data;
            Assert.IsFalse(dto.Success);
            Assert.AreEqual(msg, dto.Message);
        }
    }
}
