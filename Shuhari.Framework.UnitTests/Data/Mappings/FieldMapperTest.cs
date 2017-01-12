using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Shuhari.Framework.Data.Mappings;

namespace Shuhari.Framework.UnitTests.Data.Mappings
{
    [TestFixture]
    public class FieldMapperTest
    {
        [Test, TestCaseSource("NotNullEntity_NotNullSources")]
        public void NotNullEntity_GetValue_NotNull(string propName, object value)
        {
            AssertGet<NotNullEntity>(propName, value);
        }


        [TestCase("StringProp")]
        [TestCase("BinaryProp")]
        public void NotNullEntity_GetValue_Null(string propName)
        {
            AssertGet<NotNullEntity>(propName, null);
        }

        [Test, TestCaseSource("NotNullEntity_NotNullSources")]
        public void NotNullEntity_SetValue_NotNull(string propName, object value)
        {
            AssertSet<NotNullEntity>(propName, value);
        }

        [TestCase("StringProp")]
        [TestCase("BinaryProp")]
        public void NotNullEntity_SetValue_Null(string propName)
        {
            AssertSet<NotNullEntity>(propName, null);
        }

        [Test, TestCaseSource("NullableEntity_NotNullSources")]
        public void NullableEntity_GetValue_NotNull(string propName, object value)
        {
            AssertGet<NullableEntity>(propName, value);
        }

        [Test, TestCaseSource("NullableEntity_NullSources")]
        public void NullableEntity_GetValue_Null(string propName)
        {
            AssertGet<NullableEntity>(propName, null);
        }

        [Test, TestCaseSource("NullableEntity_NotNullSources")]
        public void NullableEntity_SetValue_NotNull(string propName, object value)
        {
            AssertSet<NullableEntity>(propName, value);
        }

        [Test, TestCaseSource("NullableEntity_NullSources")]
        public void NullableEntity_SetValue_Null(string propName)
        {
            AssertSet<NullableEntity>(propName, null);
        }

        [Test, TestCaseSource("DerivedEntity_Sources")]
        public void DerivedEntity_GetValue_NotNull(string propName, object value)
        {
            AssertGet<DerivedEntity>(propName, value);
        }

        [Test, TestCaseSource("DerivedEntity_Sources")]
        public void DerivedEntity_SetValue_NotNull(string propName, object value)
        {
            AssertSet<DerivedEntity>(propName, value);
        }

        public static IEnumerable<TestCaseData> NotNullEntity_NotNullSources
        {
            get
            {
                yield return new TestCaseData("IntProp", 123).SetName("IntProp");
                yield return new TestCaseData("ShortProp", (short)123).SetName("ShortProp");
                yield return new TestCaseData("LongProp", 123L).SetName("LongProp");
                yield return new TestCaseData("FloatProp", 123F).SetName("FloatProp");
                yield return new TestCaseData("DoubleProp", 123D).SetName("DoubleProp");
                yield return new TestCaseData("DecimalProp", 123M).SetName("DecimalProp");
                yield return new TestCaseData("BoolProp", true).SetName("BoolProp");
                yield return new TestCaseData("StringProp", "abc").SetName("StringProp");
                yield return new TestCaseData("DateTimeProp", DateTime.Now).SetName("DateTimeProp");
                yield return new TestCaseData("BinaryProp", new byte[0]).SetName("BinaryProp");
                yield return new TestCaseData("GuidProp", Guid.NewGuid()).SetName("GuidProp");
                yield return new TestCaseData("EnumProp", FileMode.Create).SetName("EnumProp");
            }
        }

        public static IEnumerable<TestCaseData> NullableEntity_NotNullSources
        {
            get
            {
                yield return new TestCaseData("IntProp", 123).SetName("IntProp");
                yield return new TestCaseData("ShortProp", (short)123).SetName("ShortProp");
                yield return new TestCaseData("LongProp", 123L).SetName("LongProp");
                yield return new TestCaseData("FloatProp", 123F).SetName("FloatProp");
                yield return new TestCaseData("DoubleProp", 123D).SetName("DoubleProp");
                yield return new TestCaseData("DecimalProp", 123M).SetName("DecimalProp");
                yield return new TestCaseData("BoolProp", true).SetName("BoolProp");
                yield return new TestCaseData("DateTimeProp", DateTime.Now).SetName("DateTimeProp");
                yield return new TestCaseData("GuidProp", Guid.NewGuid()).SetName("GuidProp");
                yield return new TestCaseData("EnumProp", FileMode.Create).SetName("EnumProp");
            }
        }

        public static IEnumerable<TestCaseData> NullableEntity_NullSources
        {
            get
            {
                yield return new TestCaseData("IntProp").SetName("IntProp");
                yield return new TestCaseData("ShortProp").SetName("ShortProp");
                yield return new TestCaseData("LongProp").SetName("LongProp");
                yield return new TestCaseData("FloatProp").SetName("FloatProp");
                yield return new TestCaseData("DoubleProp").SetName("DoubleProp");
                yield return new TestCaseData("DecimalProp").SetName("DecimalProp");
                yield return new TestCaseData("BoolProp").SetName("BoolProp");
                yield return new TestCaseData("DateTimeProp").SetName("DateTimeProp");
                yield return new TestCaseData("GuidProp").SetName("GuidProp");
                yield return new TestCaseData("EnumProp").SetName("EnumProp");
            }
        }

        public static IEnumerable<TestCaseData> DerivedEntity_NotNullSources
        {
            get
            {
                yield return new TestCaseData("CreateBy", 123).SetName("CreateBy");
                yield return new TestCaseData("CreateAt", DateTime.Now).SetName("CreateAt");
                yield return new TestCaseData("UpdateBy", 123).SetName("UpdateBy");
                yield return new TestCaseData("UpdateAt", DateTime.Now).SetName("UpdateAt");
            }
        }

        private void AssertGet<T>(string propName, object value)
            where T: class, new()
        {
            var entity = new T();
            var fieldMapper = GetFieldMapper<T>(propName);

            fieldMapper.Property.SetValue(entity, value);
            Assert.AreEqual(value, fieldMapper.GetValue(entity));
        }

        private void AssertSet<T>(string propName, object value)
            where T: class, new()
        {
            var entity = new T();
            var fieldMapper = GetFieldMapper<T>(propName);

            fieldMapper.SetValue(entity, value);
            Assert.AreEqual(value, fieldMapper.Property.GetValue(entity));
        }

        private IFieldMapper<T> GetFieldMapper<T>(string propName)
            where T: class, new()
        {
            var prop = typeof(T).GetProperty(propName);
            var mapper = MappingFactory.CreateEntityMappingFromAnnonations<T>();
            return mapper.FindByProperty(propName);
        }
    }
}
