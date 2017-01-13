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

        private static TestCaseData TestColumnOf<T, TProp>(Expression<Func<T, TProp>> selector, TProp value)
        {
            var prop = ExpressionBuilder.GetProperty(selector);
            return new TestCaseData(prop.Name, value).SetName(prop.Name);
        }

        public static IEnumerable<TestCaseData> NotNullEntity_NotNullSources
        {
            get
            {
                yield return TestColumnOf<NotNullEntity, int>(x => x.IntProp, 123);
                yield return TestColumnOf<NotNullEntity, short>(x => x.ShortProp, 123);
                yield return TestColumnOf<NotNullEntity, long>(x => x.LongProp, 123L);
                yield return TestColumnOf<NotNullEntity, float>(x => x.FloatProp, 123F);
                yield return TestColumnOf<NotNullEntity, double>(x => x.DoubleProp, 123D);
                yield return TestColumnOf<NotNullEntity, decimal>(x => x.DecimalProp, 123M);
                yield return TestColumnOf<NotNullEntity, bool>(x => x.BoolProp, true);
                yield return TestColumnOf<NotNullEntity, string>(x => x.StringProp, "abc");
                yield return TestColumnOf<NotNullEntity, DateTime>(x => x.DateTimeProp, DateTime.Now);
                yield return TestColumnOf<NotNullEntity, byte[]>(x => x.BinaryProp, new byte[0]);
                yield return TestColumnOf<NotNullEntity, Guid>(x => x.GuidProp, Guid.NewGuid());
                yield return TestColumnOf<NotNullEntity, FileMode>(x => x.EnumProp, FileMode.Create);
            }
        }

        public static IEnumerable<TestCaseData> NullableEntity_NotNullSources
        {
            get
            {
                yield return TestColumnOf<NullableEntity, int?>(x => x.IntProp, 123);
                yield return TestColumnOf<NullableEntity, short?>(x => x.ShortProp, 123);
                yield return TestColumnOf<NullableEntity, long?>(x => x.LongProp, 123L);
                yield return TestColumnOf<NullableEntity, float?>(x => x.FloatProp, 123F);
                yield return TestColumnOf<NullableEntity, double?>(x => x.DoubleProp, 123D);
                yield return TestColumnOf<NullableEntity, decimal?>(x => x.DecimalProp, 123M);
                yield return TestColumnOf<NullableEntity, bool?>(x => x.BoolProp, true);
                yield return TestColumnOf<NullableEntity, DateTime?>(x => x.DateTimeProp, DateTime.Now);
                yield return TestColumnOf<NullableEntity, Guid?>(x => x.GuidProp, Guid.NewGuid());
                yield return TestColumnOf<NullableEntity, FileMode?>(x => x.EnumProp, FileMode.Create);
            }
        }

        public static IEnumerable<TestCaseData> NullableEntity_NullSources
        {
            get
            {
                yield return TestColumnOf<NullableEntity, int?>(x => x.IntProp, null);
                yield return TestColumnOf<NullableEntity, short?>(x => x.ShortProp, null);
                yield return TestColumnOf<NullableEntity, long?>(x => x.LongProp, null);
                yield return TestColumnOf<NullableEntity, float?>(x => x.FloatProp, null);
                yield return TestColumnOf<NullableEntity, double?>(x => x.DoubleProp, null);
                yield return TestColumnOf<NullableEntity, decimal?>(x => x.DecimalProp, null);
                yield return TestColumnOf<NullableEntity, bool?>(x => x.BoolProp, null);
                yield return TestColumnOf<NullableEntity, DateTime?>(x => x.DateTimeProp, null);
                yield return TestColumnOf<NullableEntity, Guid?>(x => x.GuidProp, null);
                yield return TestColumnOf<NullableEntity, FileMode?>(x => x.EnumProp, null);
            }
        }

        public static IEnumerable<TestCaseData> DerivedEntity_NotNullSources
        {
            get
            {
                yield return TestColumnOf<DerivedEntity, int>(x => x.CreateBy, 123);
                yield return TestColumnOf<DerivedEntity, DateTime>(x => x.CreateAt, DateTime.Now);
                yield return TestColumnOf<DerivedEntity, int>(x => x.UpdateBy, 123);
                yield return TestColumnOf<DerivedEntity, DateTime>(x => x.UpdateAt, DateTime.Now);
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
