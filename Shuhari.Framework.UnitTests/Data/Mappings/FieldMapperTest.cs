using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using NUnit.Framework;
using Shuhari.Framework.Data.Mappings;
using Shuhari.Framework.Linq;

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
        public void NotNullEntity_GetValue_Null(string propName, object value = null)
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
        public void NullableEntity_GetValue_Null(string propName, object value = null)
        {
            AssertGet<NullableEntity>(propName, null);
        }

        [Test, TestCaseSource("NullableEntity_NotNullSources")]
        public void NullableEntity_SetValue_NotNull(string propName, object value)
        {
            AssertSet<NullableEntity>(propName, value);
        }

        [Test, TestCaseSource("NullableEntity_NullSources")]
        public void NullableEntity_SetValue_Null(string propName, object value = null)
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
                return new TestCaseDataBuilder<NotNullEntity>("NotNullEntity_NotNullSources")
                    .CaseOf(x => x.IntProp, 123)
                    .CaseOf(x => x.ShortProp, (short)123)
                    .CaseOf(x => x.LongProp, 123L)
                    .CaseOf(x => x.LongProp, 123L)
                    .CaseOf(x => x.FloatProp, 123F)
                    .CaseOf(x => x.DoubleProp, 123D)
                    .CaseOf(x => x.DecimalProp, 123M)
                    .CaseOf(x => x.BoolProp, true)
                    .CaseOf(x => x.StringProp, "abc")
                    .CaseOf(x => x.DateTimeProp, DateTime.Now)
                    .CaseOf(x => x.BinaryProp, new byte[0])
                    .CaseOf(x => x.GuidProp, Guid.NewGuid())
                    .CaseOf(x => x.EnumProp, FileMode.Create)
                    .Data;
            }
        }

        public static IEnumerable<TestCaseData> NullableEntity_NotNullSources
        {
            get
            {
                return new TestCaseDataBuilder<NullableEntity>("NullableEntity_NotNullSources")
                    .CaseOf(x => x.IntProp, 123)
                    .CaseOf(x => x.ShortProp, (short)123)
                    .CaseOf(x => x.LongProp, 123L)
                    .CaseOf(x => x.FloatProp, 123F)
                    .CaseOf(x => x.DoubleProp, 123D)
                    .CaseOf(x => x.DecimalProp, 123M)
                    .CaseOf(x => x.BoolProp, true)
                    .CaseOf(x => x.DateTimeProp, DateTime.Now)
                    .CaseOf(x => x.GuidProp, Guid.NewGuid())
                    .CaseOf(x => x.EnumProp, FileMode.Create)
                    .Data;
            }
        }

        public static IEnumerable<TestCaseData> NullableEntity_NullSources
        {
            get
            {
                return new TestCaseDataBuilder<NullableEntity>("NullableEntity_NullSources")
                    .CaseOf(x => x.IntProp, null)
                    .CaseOf(x => x.ShortProp, null)
                    .CaseOf(x => x.LongProp, null)
                    .CaseOf(x => x.FloatProp, null)
                    .CaseOf(x => x.DoubleProp, 123D)
                    .CaseOf(x => x.DecimalProp, null)
                    .CaseOf(x => x.BoolProp, null)
                    .CaseOf(x => x.DateTimeProp, null)
                    .CaseOf(x => x.GuidProp, null)
                    .CaseOf(x => x.EnumProp, null)
                    .Data;
            }
        }

        public static IEnumerable<TestCaseData> DerivedEntity_NotNullSources
        {
            get
            {
                return new TestCaseDataBuilder<DerivedEntity>("DerivedEntity_NotNullSources")
                    .CaseOf(x => x.CreateBy, 123)
                    .CaseOf(x => x.CreateAt, DateTime.Now)
                    .CaseOf(x => x.UpdateBy, 123)
                    .CaseOf(x => x.UpdateAt, DateTime.Now)
                    .Data;
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
            var mapper = MappingFactory.CreateEntityMappingFromAnnonations<T>();
            return mapper.FindByProperty(propName);
        }
    }

    class TestCaseDataBuilder<T>
    {
        public TestCaseDataBuilder(string prefix)
        {
            _prefix = prefix;
            _datas = new List<TestCaseData>();
        }

        private readonly string _prefix;

        private readonly List<TestCaseData> _datas;

        public TestCaseDataBuilder<T> CaseOf<TProp>(Expression<Func<T, TProp>> selector, TProp value)
        {
            var prop = ExpressionBuilder.GetProperty(selector);
            var data = new TestCaseData(prop.Name, value).SetName(string.Format("{0}.{1}", _prefix, prop.Name));
            _datas.Add(data);
            return this;
        }

        public IEnumerable<TestCaseData> Data => _datas.AsReadOnly();
    }
}
