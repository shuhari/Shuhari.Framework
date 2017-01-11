using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Utils
{
    /// <summary>
    /// Convert betwen Clr values and Db parameters in following logic.
    /// <ul>
    ///   <header>When converting clr value to db parameter:</header>
    ///   <li>null converted to DbNull;</li>
    ///   <li>Enum converted to int32;</li>
    ///   <li>Others as is;</li>
    /// </ul>
    /// <ul>
    ///   <header>When converting db parameter to clr value:</header>
    ///   <li>DbNull (and null) converted to null;</li>
    ///   <li>If target type is enum, then source (should be int32) should convert to target enum;</li>
    ///   <li>If target type is enum? and value is int32 then converted to target enum;</li>
    ///   <li>Others as is;</li>
    /// </ul>
    /// </summary>
    public static class ParamConverter
    {
        /// <summary>
        /// Convert from Clr value to db parameter value
        /// </summary>
        /// <param name="clrValue"></param>
        /// <returns></returns>
        public static object ToDbValue(object clrValue)
        {
            if (clrValue == null)
                return DBNull.Value;

            var valueType = clrValue.GetType();
            if (valueType.IsEnum)
                return Convert.ToInt32(clrValue);

            return clrValue;
        }

        /// <summary>
        /// Convert from Db parameter value to clr value
        /// </summary>
        /// <param name="dbValue"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object ToClrValue(object dbValue, Type targetType)
        {
            Expect.IsNotNull(targetType, nameof(targetType));

            if (dbValue == null || Convert.IsDBNull(dbValue))
                return null;

            var valueType = dbValue.GetType();
            if (targetType.IsEnum)
                return ConvertToEnum(dbValue, targetType);
            else if (targetType.IsNullableType() && targetType.GetNullableBaseType().IsEnum)
                return ConvertToEnum(dbValue, targetType.GetNullableBaseType());

            return dbValue;
        }

        private static object ConvertToEnum(object value, Type enumType)
        {
            Expect.IsNotNull(value, nameof(value));
            Expect.IsNotNull(enumType, nameof(enumType));
            Expect.That(enumType.IsEnum, string.Format(FrameworkStrings.ErrorTypeNotEnum, enumType));

            if (value != null && value.GetType() == typeof(int))
                return Enum.ToObject(enumType, value);
            else
                throw ExceptionBuilder.NotSupported(FrameworkStrings.ErrorUnsupportedType, value.GetType());
        }
    }
}
