using System;
using System.Linq;
using System.Reflection;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Xml.Serialization
{
    /// <summary>
    /// Define fixed Attributes that should write to xml, 
    /// but not needed to deserialize to clr object
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class XmlAdditionalAttributeAttribute : Attribute, IComparable
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="order"></param>
        public XmlAdditionalAttributeAttribute(string name, string value, int order = 0)
        {
            Expect.IsNotBlank(name, nameof(name));
            Expect.IsNotBlank(value, nameof(value));

            this.Name = name;
            this.Value = value;
            this.Order = order;
        }

        /// <summary>
        /// Attribute name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Attribute value
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Serialize order
        /// </summary>
        public int Order { get; private set; }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            var other = obj as XmlAdditionalAttributeAttribute;
            if (other == null)
                return 0;
            return Order.CompareTo(other.Order);
        }

        /// <summary>
        /// Load all matched from entity and sort them
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static XmlAdditionalAttributeAttribute[] LoadAll(Type type)
        {
            Expect.IsNotNull(type, nameof(type));

            var attrs = type.GetCustomAttributes<XmlAdditionalAttributeAttribute>()
                .ToArray();
            Array.Sort(attrs);
            return attrs;
        }
    }
}
