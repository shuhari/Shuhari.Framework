using System;
using System.IO;
using NUnit.Framework;
using Shuhari.Framework.Data.Utils;

namespace Shuhari.Framework.UnitTests.Data.Utils
{
    [TestFixture]
    public class ParamConverterTest
    {
        [Test]
        public void ToDbValue_ParamNull_ShouldReturnDbNull()
        {
            Assert.AreEqual(DBNull.Value, ParamConverter.ToDbValue(null));
        }

        [Test]
        public void ToDbValue_Enum_ShouldReturnInt()
        {
            var value = FileMode.Create;
            Assert.AreEqual((int)value, ParamConverter.ToDbValue(value));
        }

        [TestCase(1, 1)]
        [TestCase(1L, 1L)]
        [TestCase((short)1, (short)1)]
        public void ToDbValue_Others_AsIs(object clrValue, object dbValue)
        {
            Assert.AreEqual(dbValue, ParamConverter.ToDbValue(clrValue));
        }

        [Test]
        public void ToClrValue_ParamDbNull_ShouldReturnNull()
        {
            Assert.IsNull(ParamConverter.ToClrValue(DBNull.Value, typeof(string)));
        }

        [Test]
        public void ToClrValue_TargetIsEnum_ShouldConvertInt()
        {
            object dbValue = (int)FileMode.Create;

            Assert.AreEqual(FileMode.Create, ParamConverter.ToClrValue(dbValue, typeof(FileMode)));
        }

        [Test]
        public void ToClrValue_TargetIsEnum_SourceNotInt_ShouldThrow()
        {
            Assert.Throws<NotSupportedException>(() =>
                ParamConverter.ToClrValue("abc", typeof(FileMode)));
        }

        [Test]
        public void ToClrValue_TargetIsNulltableEnum_SourceInt_ShouldReturn()
        {
            var dbValue = (int)FileMode.Create;
            Assert.AreEqual(FileMode.Create, ParamConverter.ToClrValue(dbValue, typeof(FileMode?)));
        }

        [TestCase(1, typeof(int), 1)]
        [TestCase(1L, typeof(long), 1L)]
        public void ToClrValue_Others_AsIs(object dbValue, Type targetType, object clrValue)
        {
            Assert.AreEqual(clrValue, ParamConverter.ToClrValue(dbValue, targetType));
        }
    }
}
