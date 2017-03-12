using System;

namespace Shuhari.Framework.Xml.Serialization
{
    /// <summary>
    /// Implement default serialize/deserialize logic
    /// </summary>
    public class DefaultValueSerializer : IValueSerializer
    {
        /// <inheritdoc />
        public string Serialize(object value)
        {
            return Convert.ToString(value);
        }

        /// <inheritdoc />
        public object Deserialize(string str)
        {
            throw new NotImplementedException();
        }
    }
}
