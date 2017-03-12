using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Xml.Serialization
{
    /// <summary>
    /// Serialize bool value
    /// </summary>
    public class BoolValueSerializer : IValueSerializer
    {
        /// <inheritdoc />
        public string Serialize(object value)
        {
            Expect.IsNotNull(value, nameof(value));
            Expect.That(value is bool, string.Format("Expect bool bot got type={0}, value={1}",
                value.GetType().FullName, value));

            var bValue = (bool) value;
            return bValue ? "true" : "false";
        }

        /// <inheritdoc />
        public object Deserialize(string str)
        {
            Expect.IsNotNull(str, nameof(str));

            return str.EqualsNoCase("true");
        }
    }
}
