using System;
using System.Data;
using System.Linq;
using NUnit.Framework;
using Shuhari.Framework.Data.Mappings;

namespace Shuhari.Framework.UnitTests.Data.Mappings
{
    [TestFixture]
    public class SchemaTableTest
    {
        [SetUp]
        public void SetUp()
        {
            _dataTable = new DataTable();
            AddColumn(_dataTable, "ColumnName", typeof(string));
            AddColumn(_dataTable, "DataType", typeof(Type));
            AddColumn(_dataTable, "AllowDBNull", typeof(bool));
            AddRow(_dataTable, "StringProp", typeof(string), true);
            AddRow(_dataTable, "IntProp", typeof(int), false);
            AddRow(_dataTable, "DateTimeProp", typeof(DateTime), false);

            _schemaTable = new SchemaTable();
            _schemaTable.Load(_dataTable);
        }

        private DataTable _dataTable;
        private SchemaTable _schemaTable;

        private void AddColumn(DataTable table, string name, Type type)
        {
            table.Columns.Add(new DataColumn { ColumnName = name, DataType = type });
        }

        private void AddRow(DataTable table, string colName, Type type, bool allowDbNull)
        {
            var row = table.NewRow();
            row["ColumnName"] = colName;
            row["DataType"] = type;
            row["AllowDBNull"] = allowDbNull;
            table.Rows.Add(row);
        }

        [Test]
        public void Columns_ShouldReturnRowsFromDataTable()
        {
            Assert.AreEqual(_dataTable.Rows.Count, _schemaTable.Columns.Count());
        }

        [TestCase("StringProp", typeof(string), true)]
        [TestCase("IntProp", typeof(int), false)]
        [TestCase("DateTimeProp", typeof(DateTime), false)]
        public void FindColumn(string name, Type type, bool allowDbNull)
        {
            var column = _schemaTable.FindColumn(name);

            Assert.IsNotNull(column);
            Assert.AreEqual(name, column.ColumnName);
            Assert.AreEqual(type, column.DataType);
            Assert.AreEqual(allowDbNull, column.AllowDBNull);
        }

        [Test]
        public void Dump()
        {
            var content = _schemaTable.Dump();
            Assert.IsNotEmpty(content);

            // Console.WriteLine(content);
        }
    }
}
