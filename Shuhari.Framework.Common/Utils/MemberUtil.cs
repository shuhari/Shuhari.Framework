using System;
using System.Reflection;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper to process MemberInfo
    /// </summary>
    public static class MemberUtil
    {
        /// <summary>
        /// Check if member has specified attribute
        /// </summary>
        /// <param name="member"></param>
        /// <param name="attrType"></param>
        /// <returns></returns>
        public static bool HasAttribute(this MemberInfo member, Type attrType)
        {
            Expect.IsNotNull(member, nameof(member));
            Expect.IsNotNull(attrType, nameof(attrType));

            return member.GetCustomAttribute(attrType, true) != null;
        }

        /// <summary>
        /// Check if member has specified attribute
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this MemberInfo member)
            where T: Attribute
        {
            Expect.IsNotNull(member, nameof(member));

            return HasAttribute(member, typeof(T));
        }
    }
}
