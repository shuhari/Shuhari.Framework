using System;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Mappings
{
    /// <summary>
    /// If entity support <see cref="IExtensibleProperties"/>, then save property in extented properties
    /// others ignore
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class GenericFieldReader<T> : IFieldReader<T>
        where T : class
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="clrType"></param>
        public GenericFieldReader(string propName, Type clrType)
        {
            Expect.IsNotBlank(propName, nameof(propName));
            Expect.IsNotNull(clrType, nameof(clrType));

            _propName = propName;
            PropertyType = clrType;
        }

        private readonly string _propName;

        /// <inheritdoc />
        public Type PropertyType { get; private set; }

        /// <inheritdoc />
        public bool Match(SchemaColumn column)
        {
            return true;
        }

        /// <inheritdoc />
        public void SetValue(T entity, object value)
        {
            var extensible = entity as IExtensibleProperties;
            if (extensible != null)
                extensible[_propName] = value;
        }
    }
}
