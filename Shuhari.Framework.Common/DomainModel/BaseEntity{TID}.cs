using System.Collections.Generic;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Base entity
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public class BaseEntity<TId> : IEntity<TId>, IExtensibleProperties
        where TId: struct
    {
        /// <inheritdoc />
        public TId Id { get; set; }

        private Dictionary<string, object> _properties;

        /// <summary>
        /// Additional properties, to hold fields from query 
        /// who are not mapped as property
        /// </summary>
        public object this[string propName]
        {
            get
            {
                Expect.IsNotBlank(propName, nameof(propName));
                propName = NormalizePropertyName(propName);

                if (_properties == null || !_properties.ContainsKey(propName))
                    throw ExceptionBuilder.KeyNotFound(FrameworkStrings.ErrorKeyNotFound, propName);
                return _properties[propName];
            }
            set
            {
                Expect.IsNotBlank(propName, nameof(propName));
                propName = NormalizePropertyName(propName);

                if (_properties == null)
                {
                    _properties = new Dictionary<string, object>();
                }
                _properties[propName] = value;
            }
        }

        private string NormalizePropertyName(string propName)
        {
            Expect.IsNotBlank(propName, nameof(propName));

            return propName.ToLowerInvariant();
        }

        /// <summary>
        /// check if specified property exist in extended properties
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public bool HasProperty(string propName)
        {
            Expect.IsNotBlank(propName, nameof(propName));
            propName = NormalizePropertyName(propName);
            return (_properties != null && _properties.ContainsKey(propName));
        }
    }
}
