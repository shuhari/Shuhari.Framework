using System;
using System.CodeDom;
using System.IO;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class NullableUtilTest
    {
        [TestCase(typeof(int), false)]
        [TestCase(typeof(int?), true)]
        [TestCase(typeof(string), false)]
        [TestCase(typeof(FileMode), false)]
        [TestCase(typeof(FileMode?), true)]
        [TestCase(typeof(Nullable<>), false)]
        public void IsNullableType(Type type, bool result)
        {
            Assert.AreEqual(result, type.IsNullableType());
        }

        [TestCase(typeof(int))]
        [TestCase(typeof(string))]
        [TestCase(typeof(FileMode))]
        [TestCase(typeof(Nullable<>))]
        public void GetNullableBaseType_TypeNotNullable_ShouldThrow(Type type)
        {
            Assert.Throws<ExpectionException>(() => type.GetNullableBaseType());
        }

        [TestCase(typeof(int?), typeof(int))]
        [TestCase(typeof(FileMode?), typeof(FileMode))]
        public void GetNullableBaseType(Type nullableType, Type baseType)
        {
            Assert.AreEqual(baseType, nullableType.GetNullableBaseType());
        }

        [TestCase(typeof(int?))]
        [TestCase(typeof(string))]
        public void MakeNullableType_BaseTypeNotValueType_ShouldThrow(Type type)
        {
            Assert.Throws<ExpectionException>(() => type.MakeNullableType());
        }

        [TestCase(typeof(int), typeof(int?))]
        [TestCase(typeof(FileMode), typeof(FileMode?))]
        public void MakeNullableType(Type baseType, Type resultType)
        {
            Assert.AreEqual(resultType, baseType.MakeNullableType());
        }
    }
}
