using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Shuhari.Framework.Data;
using Shuhari.Framework.Data.Mappings;

namespace Shuhari.Framework.UnitTests.Data.Mappings
{
    [TestFixture]
    public class EntityReaderTest
    {
        [SetUp]
        public void SetUp()
        {
            _entity = new NotNullEntity();
            _mapper = MappingFactory.CreateEntityMappingFromAnnonations<NotNullEntity>();
        }

        private EntityMapper<NotNullEntity> _mapper;
        private NotNullEntity _entity;

        [Test]
        public void SetEntity_AllFields()
        {
            var columnNames = new[] { "FID", "FIntProp", "FShortProp", "FLongProp", "FFloatProp", "FDoubleProp",
                "FDecimalProp", "FBoolProp", "FStringProp", "FDateTimeProp", "FBinaryProp", "FGuidProp", "FEnumProp" };
            var data = new[]
            {
                new object[] { 1, 1, (short)1, 1L, 1F, 1D, 1M, true, "abc", DateTime.Now, new byte[0], Guid.NewGuid(), (int)FileMode.CreateNew },
            };
            SetEntity(null, columnNames, data);

            AssertEntityProperties(data[0]);
        }

        [Test]
        public void SetEntity_AllFields_WithAdditional()
        {
            var additionColumn = new SchemaMappingColumn("FOtherProp", typeof(string), true);
            var columnNames = new[] { "FID", "FIntProp", "FShortProp", "FLongProp", "FFloatProp", "FDoubleProp",
                "FDecimalProp", "FBoolProp", "FStringProp", "FDateTimeProp", "FBinaryProp", "FGuidProp", "FEnumProp", "FOtherProp" };
            var data = new object[][]
            {
                new object[] { 1, 1, (short)1, 1L, 1F, 1D, 1M, true, "abc", DateTime.Now, new byte[0], Guid.NewGuid(), (int)FileMode.CreateNew, "other" },
            };
            SetEntity(new[] { additionColumn }, columnNames, data);

            AssertEntityProperties(data[0]);
        }

        private void AssertEntityProperties(object[] row)
        {
            Assert.AreEqual(row[0], _entity.Id);
            Assert.AreEqual(row[1], _entity.IntProp);
            Assert.AreEqual(row[2], _entity.ShortProp);
            Assert.AreEqual(row[2], _entity.LongProp);
            if (_entity.HasProperty("FOtherProp"))
                Assert.AreEqual(row[13], _entity["FOtherProp"]);
        }

        private void SetEntity(SchemaMappingColumn[] additionalColumns, string[] sortNames, object[][] data)
        {
            additionalColumns = additionalColumns ?? new SchemaMappingColumn[0];
            var schemaMapping = DataSourceBuilder.BuildSchemaMappingFromEntityMapping(_mapper, additionalColumns);
            var columns = schemaMapping.Columns.ToArray();
            SchemaMappingColumn.SortByName(columns, sortNames);
            var dataReader = DataSourceBuilder.BuildDataReader(columns, data);
            var entityReader = new EntityReader<NotNullEntity>(_mapper, dataReader.GetSchemaTable());
            dataReader.Read();
            entityReader.SetEntity(dataReader, _entity);
        }
    }
}
