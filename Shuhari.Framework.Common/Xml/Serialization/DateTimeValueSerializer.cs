using System;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Xml.Serialization
{
    /// <summary>
    /// Serialize DateTime 
    /// </summary>
    public class DateTimeValueSerializer : IValueSerializer
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="format"></param>
        public DateTimeValueSerializer(string format = null)
        {
            _format = format ?? DEFAULT_FORMAT;
        }

        private readonly string _format;

        /// <summary>
        /// Default format
        /// </summary>
        public const string DEFAULT_FORMAT = "yyyy-MM-dd HH:mm:ss";

        /// <inheritdoc />
        public object Deserialize(string str)
        {
            Expect.IsNotBlank(str, nameof(str));

            return DateTime.ParseExact(str, _format, null);
        }

        /// <inheritdoc />
        public string Serialize(object value)
        {
            Expect.IsNotNull(value, nameof(value));
            Expect.That(value is DateTime, string.Format("Expect DateTime bot got type={0}, value={1}",
                value.GetType().FullName, value));

            var dValue = (DateTime) value;
            return dValue.ToString(_format);
        }
    }
}
