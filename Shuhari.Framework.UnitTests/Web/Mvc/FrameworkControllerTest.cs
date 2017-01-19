﻿using System;
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

        private ValidationResultDTO ExecAjax<T>(T model, Action<T> action, string successMsg)
        {
            var result = _controller.ExecuteAjax(model, action, "msg");
            return (ValidationResultDTO)result.Data;
        }

        [Test]
        public void ExecuteAjax_HasModelError_ShouldReturnError()
        {
            _controller.ModelState.AddModelError("", "test error");
            var resultData = ExecAjax("abc", x => { }, "success");

            Assert.IsFalse(resultData.Success);
            Assert.AreEqual(1, resultData.Errors.Length);
            Assert.AreEqual("", resultData.Errors[0].Property);
            Assert.AreEqual("test error", resultData.Errors[0].Message);
        }

        [Test]
        public void ExecuteAjax_ActionThrow_ShouldReturnError()
        {
            var resultData = ExecAjax("abc", x => { throw new Exception("test exception"); }, "success");

            Assert.IsFalse(resultData.Success);
            Assert.AreEqual(1, resultData.Errors.Length);
            Assert.AreEqual("", resultData.Errors[0].Property);
            Assert.AreEqual("test exception", resultData.Errors[0].Message);
        }

        [Test]
        public void ExecuteAjax_AllSuccess_ShouldReturnSuccess()
        {
            var resultData = ExecAjax("abc", x => { }, "success");

            Assert.IsTrue(resultData.Success);
        }
    }
}