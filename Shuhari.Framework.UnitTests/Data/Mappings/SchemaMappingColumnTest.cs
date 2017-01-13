using System;
using System.Linq;
using NUnit.Framework;
using Shuhari.Framework.Data;
using Shuhari.Framework.Data.Mappings;

namespace Shuhari.Framework.UnitTests.Data.Mappings
{
    [TestFixture]
    public class SchemaMappingColumnTest
    {
        [TestCase(typeof(int), false, typeof(int))]
        [TestCase(typeof(int), true, typeof(int?))]
        [TestCase(typeof(string), false, typeof(string))]
        [TestCase(typeof(string), true, typeof(string))]
        public void ClrType(Type dataType, bool allowDbNull, Type clrType)
        {
            var column = new SchemaMappingColumn("column", dataType, allowDbNull);
            Assert.AreEqual(clrType, column.ClrType);
        }

        [Test]
        public void SortByName()
        {
            var entityMapper = MappingFactory.CreateEntityMappingFromAnnonations<NotNullEntity>();
            var schemaTable = DataSourceBuilder.BuildSchemaTableFromEntityMapping(entityMapper);
            var schemaMapping = new SchemaMapping();
            schemaMapping.Load(schemaTable);
            var columns = schemaMapping.Columns.ToArray();
            SchemaMappingColumn.SortByName(columns, "FID", "FIntProp", "FLongProp");

            Assert.AreEqual("FID", columns[0].ColumnName);
        }
    }
}
