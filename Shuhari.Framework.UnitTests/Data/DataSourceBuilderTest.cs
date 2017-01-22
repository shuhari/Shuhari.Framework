using System;
using System.Data;
using System.Linq;
using NUnit.Framework;
using Shuhari.Framework.Data;
using Shuhari.Framework.Data.Mappings;

namespace Shuhari.Framework.UnitTests.Data
{
    [TestFixture]
    public class DataSourceBuilderTest
    {
        private SchemaMappingColumn[] TestColumns
        {
            get
            {
                return new[]
                {
                    new SchemaMappingColumn("IntProp", typeof(int), false),
                    new SchemaMappingColumn("StringProp", typeof(string), true),
                    new SchemaMappingColumn("DateTimeProp", typeof(DateTime), false)
                };
            }
        }

        [Test]
        public void BuildSchemaTable()
        {
            var table = DataSourceBuilder.BuildSchemaTable(TestColumns);

            AssertSchemaTable(table, TestColumns);
        }

        private void AssertSchemaTable(DataTable table, SchemaMappingColumn[] columns)
        {
            AssertSchemaColumn(table, "ColumnName", typeof(string));
            AssertSchemaColumn(table, "DataType", typeof(Type));
            AssertSchemaColumn(table, "AllowDBNull", typeof(bool));

            foreach (var column in columns)
            {
                var row = table.Rows.OfType<DataRow>().FirstOrDefault(x => x["ColumnName"].Equals(column.ColumnName));
                Assert.IsNotNull(row, "Row not found: " + column.ColumnName);
                Assert.AreEqual(column.DataType, row["DataType"]);
                Assert.AreEqual(column.AllowDBNull, row["AllowDBNull"]);
            }
        }

        private void AssertSchemaColumn(DataTable table, string columnName, Type dataType)
        {
            var column = table.Columns.OfType<DataColumn>().FirstOrDefault(x => x.ColumnName == columnName);
            Assert.IsNotNull(column, "Column not exist: " + columnName);
            Assert.AreEqual(dataType, column.DataType);
        }

        [Test]
        public void BuildSchemaTableFromMapping()
        {
            var entityMapping = MappingFactory.CreateEntityMappingFromAnnonations<NotNullEntity>();
            var schemaTable = DataSourceBuilder.BuildSchemaTableFromEntityMapping(entityMapping);
            AssertSchemaTable(schemaTable, new[]
            {
                new SchemaMappingColumn("FID", typeof(int), false),
                new SchemaMappingColumn("FIntProp", typeof(int), false),
                new SchemaMappingColumn("FShortProp", typeof(short), false),
                new SchemaMappingColumn("FLongProp", typeof(long), false),
                new SchemaMappingColumn("FFloatProp", typeof(float), false),
                new SchemaMappingColumn("FDoubleProp", typeof(double), false),
                new SchemaMappingColumn("FDecimalProp", typeof(decimal), false),
                new SchemaMappingColumn("FBoolProp", typeof(bool), false),
                new SchemaMappingColumn("FStringProp", typeof(string), true),
                new SchemaMappingColumn("FDateTimeProp", typeof(DateTime), false),
                new SchemaMappingColumn("FBinaryProp", typeof(byte[]), true),
                new SchemaMappingColumn("FGuidProp", typeof(Guid), false),
                new SchemaMappingColumn("FEnumProp", typeof(int), false),
            });
        }

        [Test]
        public void BuildSchemaTableFromMapping_WithNonMapped()
        {
            var entityMapping = MappingFactory.CreateEntityMappingFromAnnonations<NotNullEntity>();
            var schemaTable = DataSourceBuilder.BuildSchemaTableFromEntityMapping(entityMapping,
                new SchemaMappingColumn("OtherProp", typeof(string), true));
            AssertSchemaTable(schemaTable, new[]
            {
                new SchemaMappingColumn("FID", typeof(int), false),
                new SchemaMappingColumn("FIntProp", typeof(int), false),
                new SchemaMappingColumn("FShortProp", typeof(short), false),
                new SchemaMappingColumn("FLongProp", typeof(long), false),
                new SchemaMappingColumn("FFloatProp", typeof(float), false),
                new SchemaMappingColumn("FDoubleProp", typeof(double), false),
                new SchemaMappingColumn("FDecimalProp", typeof(decimal), false),
                new SchemaMappingColumn("FBoolProp", typeof(bool), false),
                new SchemaMappingColumn("FStringProp", typeof(string), true),
                new SchemaMappingColumn("FDateTimeProp", typeof(DateTime), false),
                new SchemaMappingColumn("FBinaryProp", typeof(byte[]), true),
                new SchemaMappingColumn("FGuidProp", typeof(Guid), false),
                new SchemaMappingColumn("FEnumProp", typeof(int), false),
                new SchemaMappingColumn("OtherProp", typeof(string), true)
            });
        }

        [Test]
        public void BuildDataReader()
        {
            var columns = new[]
            {
                new SchemaMappingColumn("IntProp", typeof(int), false),
                new SchemaMappingColumn("StringProp", typeof(string), true),
                new SchemaMappingColumn("DateTimeProp", typeof(DateTime), false)
            };
            var data = new object[][]
            {
                new object[] { 1, "row1", DateTime.Now },
                new object[] { 2, "row2", DateTime.Now },
            };
            var reader = DataSourceBuilder.BuildDataReader(TestColumns, data);
            var schema = reader.GetSchemaTable();

            AssertSchemaTable(schema, TestColumns);

            Assert.IsTrue(reader.Read());
            CollectionAssert.AreEqual(data[0], new object[] { reader[0], reader[1], reader[2] });

            Assert.IsTrue(reader.Read());
            CollectionAssert.AreEqual(data[1], new object[] { reader[0], reader[1], reader[2] });

            Assert.IsFalse(reader.Read());
        }
    }
}
