using NUnit.Framework;
using Shuhari.Framework.Data.Mappings;

namespace Shuhari.Framework.UnitTests.Data.Mappings
{
    [TestFixture]
    public class MappingFactoryTest
    {
        [Test]
        public void EntityMapping_NotNull_TableInfo()
        {
            var mapper = MappingFactory.CreateEntityMappingFromAnnonations<NotNullEntity>();
            Assert.AreEqual("TNotNullEntity", mapper.TableName);
        }

        [Test]
        public void EntityMapping_NotNull_PrimaryKey()
        {
            AssertPrimaryKey<NotNullEntity>("FID", true);
        }

        [TestCase("IntProp", "FIntProp", true, true)]
        [TestCase("ShortProp", "FShortProp", true, true)]
        [TestCase("LongProp", "FLongProp", true, true)]
        [TestCase("FloatProp", "FFloatProp", true, true)]
        [TestCase("DoubleProp", "FDoubleProp", true, true)]
        [TestCase("DecimalProp", "FDecimalProp", true, true)]
        [TestCase("BoolProp", "FBoolProp", true, true)]
        [TestCase("StringProp", "FStringProp", true, true)]
        [TestCase("DateTimeProp", "FDateTimeProp", true, true)]
        [TestCase("BinaryProp", "FBinaryProp", true, true)]
        [TestCase("GuidProp", "FGuidProp", true, true)]
        [TestCase("EnumProp", "FEnumProp", true, true)]
        public void EntityMapping_NotNull_Fields(string propName, string fieldName, bool insert, bool update)
        {
            AssertFieldMapping<NotNullEntity>(propName, fieldName, insert, update);
        }

        [Test]
        public void EntityMapping_Derived_TableInfo()
        {
            var mapper = MappingFactory.CreateEntityMappingFromAnnonations<DerivedEntity>();
            Assert.AreEqual("TDerivedEntity", mapper.TableName);
        }

        [Test]
        public void EntityMapping_Derived_PrimaryKey()
        {
            AssertPrimaryKey<DerivedEntity>("FID", true);
        }

        [TestCase("CreateBy", "FCreateBy", true, false)]
        [TestCase("CreateAt", "FCreateAt", true, false)]
        [TestCase("UpdateBy", "FUpdateBy", true, true)]
        [TestCase("UpdateAt", "FUpdateAt", true, true)]
        public void EntityMapping_Derived_Fields(string propName, string fieldName, bool insert, bool update)
        {
            AssertFieldMapping<DerivedEntity>(propName, fieldName, insert, update);
        }

        [Test]
        public void FieldMappings_ShouldIgnoreNotMapped()
        {
            var mapper = MappingFactory.CreateEntityMappingFromAnnonations<NotNullEntity>();
            Assert.IsNull(mapper.FindByProperty("NotMappedProp"));
        }

        private void AssertPrimaryKey<T>(string fieldName, bool identity)
            where T: class
        {
            var mapper = MappingFactory.CreateEntityMappingFromAnnonations<T>();
            var pk = mapper.GetPrimaryKey();

            Assert.IsNotNull(pk);
            Assert.AreEqual(PrimaryKeyAttribute.PROPERTY_NAME, pk.PropertyName);
            Assert.AreEqual(fieldName, pk.FieldName);
            Assert.IsTrue(pk.IsPrimaryKey);
            Assert.AreEqual(identity, pk.Identity);
            Assert.IsFalse(pk.Insert);
            Assert.IsFalse(pk.Update);
        }

        private void AssertFieldMapping<T>(string propName, string fieldName, 
            bool insert, bool update)
            where T: class
        {
            var mapper = MappingFactory.CreateEntityMappingFromAnnonations<T>();
            var prop = typeof(T).GetProperty(propName);
            var fm = mapper.FindByProperty(propName);

            Assert.IsNotNull(fm);
            Assert.AreEqual(prop, fm.Property);
            Assert.AreEqual(fieldName, fm.FieldName);
            Assert.AreEqual(insert, fm.Insert);
            Assert.AreEqual(update, fm.Update);
        }
    }
}
