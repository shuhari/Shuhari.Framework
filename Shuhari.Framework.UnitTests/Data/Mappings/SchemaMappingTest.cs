using System;
using System.Data;
using System.Linq;
using NUnit.Framework;
using Shuhari.Framework.Data;
using Shuhari.Framework.Data.Mappings;

namespace Shuhari.Framework.UnitTests.Data.Mappings
{
    [TestFixture]
    public class SchemaMappingTest
    {
        [SetUp]
        public void SetUp()
        {
            _dataTable = DataSourceBuilder.BuildSchemaTable(
                new SchemaMappingColumn("StringProp", typeof(string), true),
                new SchemaMappingColumn("IntProp", typeof(int), false),
                new SchemaMappingColumn("DateTimeProp", typeof(DateTime), false)
            );

            _mapping = new SchemaMapping();
            _mapping.Load(_dataTable);
        }

        private DataTable _dataTable;
        private SchemaMapping _mapping;

        [Test]
        public void Columns_ShouldReturnRowsFromDataTable()
        {
            Assert.AreEqual(_dataTable.Rows.Count, _mapping.Columns.Count());
        }

        [TestCase("StringProp", typeof(string), true)]
        [TestCase("IntProp", typeof(int), false)]
        [TestCase("DateTimeProp", typeof(DateTime), false)]
        public void FindColumn(string name, Type type, bool allowDbNull)
        {
            var column = _mapping.FindColumn(name);

            Assert.IsNotNull(column);
            Assert.AreEqual(name, column.ColumnName);
            Assert.AreEqual(type, column.DataType);
            Assert.AreEqual(allowDbNull, column.AllowDBNull);
        }

        [Test]
        public void Dump()
        {
            var content = _mapping.Dump();
            Assert.IsNotEmpty(content);

            // Console.WriteLine(content);
        }
    }
}
