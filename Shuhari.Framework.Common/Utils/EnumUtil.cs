using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Resources;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper to process enum
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// Get display name of enum field. Field should annotated with DisplayAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayName<T>(T value)
            where T : struct
        {
            var type = typeof(T);
            Expect.That(type.IsEnum, string.Format(FrameworkStrings.ErrorTypeNotEnum, type.FullName));
            var nValue = Convert.ToInt32(value);

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            if (type.GetCustomAttribute<FlagsAttribute>() != null)
            {
                var names = new List<string>();
                foreach (var field in fields)
                {
                    var fieldValue = Convert.ToInt32(field.GetValue(null));
                    if (fieldValue != 0 && (nValue & fieldValue) == fieldValue)
                        names.Add(GetDisplayName(field));
                }
                return string.Join(",", names);
            }
            else
            {
                foreach (var field in fields)
                {
                    var fieldValue = Convert.ToInt32(field.GetValue(null));
                    if (fieldValue == nValue)
                        return GetDisplayName(field);
                }
                return "";
            }
        }

        private static string GetDisplayName(FieldInfo field)
        {
            var attr = field.GetCustomAttribute<DisplayAttribute>();
            if (attr != null && attr.Name.IsNotBlank())
                return attr.Name;
            return field.Name;
        }
    }
}
