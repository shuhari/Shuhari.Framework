using System;
using System.Globalization;
using System.Windows.Markup;

namespace Shuhari.Framework.ComponentModel
{
    /// <summary>
    /// Used in xaml serialization to standarlize date time format
    /// </summary>
    public class DefaultDateTimeValueSerializer : ValueSerializer
    {
        /// <summary>
        /// Datetime format
        /// </summary>
        public const string DEFAULT_FORMAT = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Datetime format
        /// </summary>
        public static string Format { get; set; } = DEFAULT_FORMAT;

        /// <inheritdoc />
        public override bool CanConvertFromString(string value, IValueSerializerContext context)
        {
            if (value != null)
            {
                DateTime dt;
                if (DateTime.TryParseExact(value, Format, null, DateTimeStyles.None, out dt))
                    return true;
            }
            return base.CanConvertFromString(value, context);
        }

        /// <inheritdoc />
        public override object ConvertFromString(string value, IValueSerializerContext context)
        {
            if (value != null)
            {
                DateTime dt;
                if (DateTime.TryParseExact(value, Format, null, DateTimeStyles.None, out dt))
                    return dt;
            }
            return base.ConvertFromString(value, context);
        }

        /// <inheritdoc />
        public override bool CanConvertToString(object value, IValueSerializerContext context)
        {
            if (value is DateTime)
                return true;
            return base.CanConvertToString(value, context);
        }

        /// <inheritdoc />
        public override string ConvertToString(object value, IValueSerializerContext context)
        {
            if (value is DateTime)
            {
                var dtValue = (DateTime)value;
                return dtValue.ToString(Format);
            }
            return base.ConvertToString(value, context);
        }
    }
}
