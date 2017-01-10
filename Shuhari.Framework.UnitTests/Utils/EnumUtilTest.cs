using System;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class EnumUtilTest
    {
        enum TestEnum
        {
            [Display(Name = "MyValue1")]
            Value1,

            Value2,
        }

        [Flags]
        enum FlaggedEnum
        {
            [Display(Name = "MyBit1")]
            Bit1 = 0x1,

            [Display(Name = "MyBit2")]
            Bit2 = 0x2,
        }

        [Test]
        public void GetDisplayName_HasDisplayAttribute_ShouldReturnAnnonated()
        {
            Assert.AreEqual("MyValue1", EnumUtil.GetDisplayName(TestEnum.Value1));
        }

        [Test]
        public void GetDisplayName_NoDisplayAttribute_ShouldReturnFieldName()
        {
            Assert.AreEqual("Value2", EnumUtil.GetDisplayName(TestEnum.Value2));
        }

        [Test]
        public void GetDisplayName_WithFlags_ShouldReturnCombined()
        {
            Assert.AreEqual("MyBit1,MyBit2", EnumUtil.GetDisplayName(FlaggedEnum.Bit1 | FlaggedEnum.Bit2));
        }
    }
}
