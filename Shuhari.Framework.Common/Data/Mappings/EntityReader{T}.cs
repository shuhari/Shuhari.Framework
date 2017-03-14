using System.Data;
using System.Linq;
using Shuhari.Framework.Data.Utils;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Use field readers to get value from data reader
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class EntityReader<T> 
        where T: class
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="entityMapper"></param>
        /// <param name="schemaTable"></param>
        public EntityReader(EntityMapper<T> entityMapper, DataTable schemaTable)
        {
            Expect.IsNotNull(entityMapper, nameof(entityMapper));
            Expect.IsNotNull(schemaTable, nameof(schemaTable));

            var schema = new SchemaMapping();
            schema.Load(schemaTable);

            _fieldReaders = schema.Columns.Select(entityMapper.GetFieldReader).ToArray();
        }

        private readonly IFieldReader<T>[] _fieldReaders;

        /// <summary>
        /// Set entity properties
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        public void SetEntity(IDataReader reader, T entity)
        {
            Expect.IsNotNull(reader, nameof(reader));
            Expect.IsNotNull(entity, nameof(entity));
            if (reader.FieldCount != _fieldReaders.Length)
                throw ExceptionBuilder.Mapping(FrameworkStrings.ErrorFieldCountNotMatch, reader.FieldCount, _fieldReaders.Length);

            for (int i = 0; i < _fieldReaders.Length; i++)
            {
                var fieldReader = _fieldReaders[i];
                var value = ParamConverter.ToClrValue(reader[i], fieldReader.PropertyType);
                fieldReader.SetValue(entity, value);
            }
        }
    }
}
