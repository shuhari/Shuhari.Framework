using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Implementation of <see cref="IEntityMapper{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityMapper<T> : IEntityMapper<T>
        where T : class
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public EntityMapper()
        {
        }

        /// <inheritdoc />
        public string TableName { get; internal set; }

        /// <inheritdoc />
        public IEnumerable<IFieldMapper<T>> FieldMappers { get; private set; }

        /// <inheritdoc />
        public IFieldMapper<T> GetPrimaryKey()
        {
            return FieldMappers.SingleOrDefault(x => x.IsPrimaryKey);
        }

        /// <summary>
        /// Load mapping from entity
        /// </summary>
        internal void Load()
        {
            LoadMapping();
            LoadFieldMappings();
        }

        private void LoadMapping()
        {
            var tableAttr = typeof(T).GetCustomAttribute<TableAttribute>(true);
            if (tableAttr != null && tableAttr.TableName.IsNotBlank())
                TableName = tableAttr.TableName;
            else
                TableName = typeof(T).Name;
        }

        private void LoadFieldMappings()
        {
            var entityType = typeof(T);

            var fieldMappers = new List<IFieldMapper<T>>();
            fieldMappers.Add(FieldMapper<T>.CreatePrimaryKey(entityType));
            fieldMappers.AddRange(entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(FieldMapper<T>.Create)
                .Where(x => x != null));

            this.FieldMappers = fieldMappers.AsReadOnly();
        }

        /// <summary>
        /// Find field by property name
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public IFieldMapper<T> FindByProperty(string propName)
        {
            Expect.IsNotBlank(propName, nameof(propName));

            return FieldMappers.FirstOrDefault(x => x.PropertyName == propName);
        }

        /// <inheritdoc />
        public object GetSchema(IDataReader reader)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Map(IDataReader reader, T entity, object schema)
        {
            throw new NotImplementedException();
        }
    }
}
