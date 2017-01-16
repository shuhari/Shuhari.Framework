using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
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

        [Test]
        public void ToFlags()
        {
            var flags = EnumUtil.ToFlags(FileAttributes.Hidden | FileAttributes.System,
                new[]
                {
                    FileAttributes.Hidden,
                    FileAttributes.System,
                    FileAttributes.Archive,
                    FileAttributes.ReadOnly
                });
            CollectionAssert.Contains(flags, FileAttributes.Hidden);
            CollectionAssert.Contains(flags, FileAttributes.System);
            CollectionAssert.DoesNotContain(flags, FileAttributes.ReadOnly);
        }

        [Test]
        public void FromFlags()
        {
            var flags = new[] { FileAttributes.Hidden, FileAttributes.System };
            Assert.AreEqual(FileAttributes.Hidden | FileAttributes.System, EnumUtil.FromFlags(flags));
        }
    }
}
