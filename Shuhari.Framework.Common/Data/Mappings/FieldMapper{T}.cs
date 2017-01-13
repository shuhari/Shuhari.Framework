using System;
using System.Reflection;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// Implementationof <see cref="IFieldMapper{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FieldMapper<T> : IFieldMapper<T>
        where T : class
    {
        /// <summary>
        /// Create primary key mapping
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        internal static FieldMapper<T> CreatePrimaryKey(Type entityType)
        {
            Expect.IsNotNull(entityType, nameof(entityType));

            var pkAttr = entityType.GetCustomAttribute<PrimaryKeyAttribute>(true);
            if (pkAttr == null)
                throw ExceptionBuilder.Mapping(FrameworkStrings.ErrorPrimaryKeyNotFound, entityType.FullName);
            var property = entityType.GetProperty(PrimaryKeyAttribute.PROPERTY_NAME);
            if (property == null)
                throw ExceptionBuilder.Mapping(FrameworkStrings.ErrorPrimaryKeyNotFound, entityType.FullName);

            var mapper = CreateFrom(property, pkAttr);
            mapper.IsPrimaryKey = true;
            return mapper;
        }

        /// <summary>
        /// create from mapped property, or null if property is not mapped to field
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        internal static FieldMapper<T> Create(PropertyInfo prop)
        {
            Expect.IsNotNull(prop, nameof(prop));

            var fieldAttr = prop.GetCustomAttribute<FieldAttribute>();
            if (fieldAttr == null)
                return null;

            return CreateFrom(prop, fieldAttr);
        }

        private static FieldMapper<T> CreateFrom(PropertyInfo prop, FieldAttributeBase attr)
        {
            Expect.IsNotNull(prop, nameof(prop));
            Expect.IsNotNull(attr, nameof(attr));

            var mapper = new FieldMapper<T>();
            mapper.Property = prop;
            mapper.FieldName = attr.FieldName.IsNotBlank() ? attr.FieldName : prop.Name;
            mapper.Flags = attr.Flags;

            return mapper;
        }

        private PropertyInfo _prop;
        private Delegate _getter;
        private Delegate _setter;

        /// <inheritdoc />
        public PropertyInfo Property
        {
            get
            {
                Expect.IsNotNull(_prop, nameof(_prop));
                return _prop;
            }
            private set
            {
                Expect.IsNotNull(value, nameof(value));
                _prop = value;
                _getter = ExpressionBuilder.BuildGetter(value);
                _setter = ExpressionBuilder.BuildSetter(value);
            }
        }

        /// <inheritdoc />
        public string FieldName { get; private set; }

        /// <summary>
        /// Field flags
        /// </summary>
        public FieldFlags Flags { get; private set; }

        /// <inheritdoc />
        public Type PropertyType
        {
            get { return Property.PropertyType; }
        }

        /// <inheritdoc />
        public string PropertyName
        {
            get { return Property.Name; }
        }

        /// <inheritdoc />
        public bool Identity
        {
            get { return Flags.HasFlag(FieldFlags.Identity); }
        }

        /// <inheritdoc />
        public bool Insert
        {
            get { return Flags.HasFlag(FieldFlags.Insert) && !Flags.HasFlag(FieldFlags.Identity); }
        }

        /// <inheritdoc />
        public bool Update
        {
            get { return Flags.HasFlag(FieldFlags.Update) && !Flags.HasFlag(FieldFlags.Identity); }
        }

        /// <inheritdoc />
        public bool IsPrimaryKey { get; private set; }

        /// <inheritdoc />
        public object GetValue(T entity)
        {
            Expect.IsNotNull(entity, nameof(entity));

            return _getter.DynamicInvoke(entity);
        }

        /// <inheritdoc />
        public bool Match(SchemaMappingColumn column)
        {
            if (!FieldName.EqualsNoCase(column.ColumnName))
                return false;

            var columnType = column.ClrType;
            if (PropertyType.IsNullableType() && columnType.IsNullableType())
            {
                if (MatchType(PropertyType.GetNullableBaseType(), columnType.GetNullableBaseType()))
                    return true;
            }

            return MatchType(PropertyType, columnType);
        }

        /// <summary>
        /// Enum mapped to int, others as is
        /// </summary>
        /// <param name="columnType"></param>
        /// <param name="propType"></param>
        /// <returns></returns>
        private bool MatchType(Type propType, Type columnType)
        {
            Expect.IsNotNull(columnType, nameof(columnType));
            Expect.IsNotNull(propType, nameof(propType));

            if (columnType == typeof(int) && propType.IsEnum)
                return true;
            return columnType == propType;
        }

        /// <inheritdoc />
        public void SetValue(T entity, object value)
        {
            Expect.IsNotNull(entity, nameof(entity));

            _setter.DynamicInvoke(entity, value);
        }
    }
}
