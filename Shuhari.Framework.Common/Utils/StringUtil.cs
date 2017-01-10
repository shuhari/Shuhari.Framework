using System;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper functions to process string.
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// Check if the string is blank. Same as <seealso cref="string.IsNullOrWhiteSpace(string)"/>
        /// but can be a little shorter by using extension method.
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns></returns>
        public static bool IsBlank(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Check if the string is not blank. Opposite to <see cref="IsBlank(string)"/>.
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns></returns>
        public static bool IsNotBlank(this string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Compare two string,  ignoring upper/lowercase.
        /// </summary>
        /// <param name="lhs">String to compare</param>
        /// <param name="rhs">String to compare</param>
        /// <returns></returns>
        public static bool EqualsNoCase(this string lhs, string rhs)
        {
            return string.Equals(lhs, rhs, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}