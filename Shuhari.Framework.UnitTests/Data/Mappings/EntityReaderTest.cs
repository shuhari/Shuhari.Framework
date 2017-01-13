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
            var schemaMapping = DataSourceBuilder.BuildSchemaMappingFromEntityMapping(_mapper);
            var columns = schemaMapping.Columns.ToArray();
            SchemaMappingColumn.SortByName(columns, "FID", "FIntProp", "FShortProp", "FLongProp", "FFloatProp", "FDoubleProp",
                "FDecimalProp", "FBoolProp", "FStringProp", "FDateTimeProp", "FBinaryProp", "FGuidProp", "FEnumProp");
            var data = new object[][]
            {
                new object[] { 1, 1, (short)1, 1L, 1F, 1D, 1M, true, "abc", DateTime.Now, new byte[0], Guid.NewGuid(), (int)FileMode.CreateNew },
            };
            var dataReader = DataSourceBuilder.BuildDataReader(columns, data);
            var entityReader = new EntityReader<NotNullEntity>(_mapper, dataReader.GetSchemaTable());
            dataReader.Read();
            entityReader.SetEntity(dataReader, _entity);

            Assert.AreEqual(data[0][0], _entity.Id);
            Assert.AreEqual(data[0][1], _entity.IntProp);
            Assert.AreEqual(data[0][2], _entity.ShortProp);
            Assert.AreEqual(data[0][2], _entity.LongProp);
        }

        [Test]
        public void SetEntity_AllFields_WithAdditional()
        {
            var schemaMapping = DataSourceBuilder.BuildSchemaMappingFromEntityMapping(_mapper, 
                new SchemaMappingColumn("FOtherProp", typeof(string), true));
            var columns = schemaMapping.Columns.ToArray();
            SchemaMappingColumn.SortByName(columns, "FID", "FIntProp", "FShortProp", "FLongProp", "FFloatProp", "FDoubleProp",
                "FDecimalProp", "FBoolProp", "FStringProp", "FDateTimeProp", "FBinaryProp", "FGuidProp", "FEnumProp", "FOtherProp");
            var data = new object[][]
            {
                new object[] { 1, 1, (short)1, 1L, 1F, 1D, 1M, true, "abc", DateTime.Now, new byte[0], Guid.NewGuid(), (int)FileMode.CreateNew, "other" },
            };
            var dataReader = DataSourceBuilder.BuildDataReader(columns, data);
            var entityReader = new EntityReader<NotNullEntity>(_mapper, dataReader.GetSchemaTable());
            dataReader.Read();
            entityReader.SetEntity(dataReader, _entity);

            Assert.AreEqual(data[0][0], _entity.Id);
            Assert.AreEqual(data[0][1], _entity.IntProp);
            Assert.AreEqual(data[0][2], _entity.ShortProp);
            Assert.AreEqual(data[0][2], _entity.LongProp);
            Assert.AreEqual(data[0][13], _entity["FOtherProp"]);
        }
    }
}
