using System;
using System.Collections.Generic;
using System.Reflection;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Xml.Serialization
{
    /// <summary>
    /// Save attribute info
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XmlAttributeInfo<T> : IComparable
        where T: Attribute
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="attr"></param>
        /// <param name="order"></param>
        public XmlAttributeInfo(PropertyInfo prop, T attr, int order)
        {
            Expect.IsNotNull(prop, nameof(prop));
            Expect.IsNotNull(attr, nameof(attr));

            Property = prop;
            Attribute = attr;
            Order = order;
        }

        /// <summary>
        /// Property
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Attribute
        /// </summary>
        public T Attribute { get; }

        /// <summary>
        /// Order
        /// </summary>
        public int Order { get; }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            var other = obj as XmlAttributeInfo<T>;
            if (other == null)
                return 0;
            return Order.CompareTo(other.Order);
        }

        /// <summary>
        /// Load all matched from entity and sort them
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static XmlAttributeInfo<T>[] LoadAll(Type type)
        {
            Expect.IsNotNull(type, nameof(type));

            var result = new List<XmlAttributeInfo<T>>();
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var attr = prop.GetCustomAttribute<T>();
                if (attr != null)
                {
                    var orderAttr = prop.GetCustomAttribute<XmlOrderAttribute>();
                    int order = (orderAttr != null) ? orderAttr.Order : 0;
                    result.Add(new XmlAttributeInfo<T>(prop, attr, order));
                }
            }

            result.Sort();
            return result.ToArray();
        }
    }
}
