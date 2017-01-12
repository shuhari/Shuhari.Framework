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
        /// <param name="reader"></param>
        public EntityReader(IEntityMapper<T> entityMapper, IDataReader reader)
        {
            Expect.IsNotNull(entityMapper, nameof(entityMapper));
            Expect.IsNotNull(reader, nameof(reader));

            var schema = new SchemaTable();
            schema.Load(reader.GetSchemaTable());

            _entityMapper = entityMapper;
            _fieldReaders = schema.Columns.Select(GetFieldReader).ToArray();
        }

        private IEntityMapper<T> _entityMapper;

        private IFieldReader<T>[] _fieldReaders;

        private IFieldReader<T> GetFieldReader(SchemaColumn column)
        {
            Expect.IsNotNull(column, nameof(column));

            var fieldMapper = _entityMapper.FieldMappers.FirstOrDefault(x => x.Match(column));
            if (fieldMapper != null)
                return fieldMapper;
            else
                return new GenericFieldReader<T>(column.ColumnName, column.ClrType);
        }

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
