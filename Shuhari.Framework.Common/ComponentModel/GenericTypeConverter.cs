using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.ComponentModel
{
    /// <summary>
    /// A generic type converter implemententation. It use ToString() to convert value to string,
    /// and use static T Parse(string) method to convert from string
    /// </summary>
    /// <typeparam name="T">Target class</typeparam>
    public class GenericTypeConverter<T> : TypeConverter
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public GenericTypeConverter()
        {
            // Find static T Parse(string) method
            _parseMethod = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .SingleOrDefault(IsParseMethod);
            Expect.That(_parseMethod != null, () => 
                ExceptionBuilder.NotSupported(FrameworkStrings.ErrorParseMethodNotExist, typeof(T).FullName));
        }

        private readonly MethodInfo _parseMethod;

        private static bool IsParseMethod(MethodInfo method)
        {
            Expect.IsNotNull(method, nameof(method));

            return method.IsStatic &&
                method.GetParameters().Length == 1 &&
                method.GetParameters()[0].ParameterType == typeof(string) &&
                method.ReturnType == typeof(T);
        }

        /// <inheritdoc />
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
                return _parseMethod.Invoke(null, new[] { value });
            return base.ConvertFrom(context, culture, value);
        }

        /// <inheritdoc />
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value != null)
            {
                return value.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
