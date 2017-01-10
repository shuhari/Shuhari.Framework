using System;
using Shuhari.Framework.Resources;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper function to process <see cref="Nullable{T}"/>
    /// </summary>
    public static class NullableUtil
    {
        /// <summary>
        /// Check if a type is nullable{T}
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableType(this Type type)
        {
            Expect.IsNotNull(type, nameof(type));

            return type.IsGenericType &&
                   !type.IsGenericTypeDefinition &&
                   type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Convert value type to Nullable{T}, origin type for reference types
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type MakeNullableType(this Type type)
        {
            Expect.IsNotNull(type, nameof(type));
            Expect.That(type.IsValueType && !type.IsNullableType(), string.Format(FrameworkStrings.ERROR_EXPECT_VALUETYPE, type.FullName));

            return typeof(Nullable<>).MakeGenericType(type);
        }

        /// <summary>
        /// Given type is Nullable{T}, get type {T}
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetNullableBaseType(this Type type)
        {
            Expect.IsNotNull(type, nameof(type));
            Expect.That(type.IsNullableType(), string.Format(FrameworkStrings.ERROR_NOT_NULLABLE, type.FullName));

            return type.GetGenericArguments()[0];
        }
    }
}