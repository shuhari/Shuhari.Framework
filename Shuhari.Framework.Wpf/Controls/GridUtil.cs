using System;
using System.Linq;
using System.Windows;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Wpf.Controls
{
    /// <summary>
    /// Helper for grid
    /// </summary>
    public static class GridUtil
    {
        /// <summary>
        /// Parse string to GridLength.
        /// <ul>
        ///   <li>Auto, @ parsed to Auto</li>
        ///   <li>[number]* parsed to Star</li>
        ///   <li>number parsed to Pixel</li>
        ///   <li>Other raise FormatException</li>
        /// </ul>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static GridLength Parse(string str)
        {
            Expect.IsNotBlank(str, nameof(str));

            str = str.Trim();
            if (str.EqualsNoCase("auto") || str.EqualsNoCase("@"))
                return GridLength.Auto;
            else if (str.EndsWith("*"))
            {
                var prefix = str.Substring(0, str.Length - 1).Trim();
                double value = 0;
                if (prefix.IsBlank())
                    return new GridLength(1, GridUnitType.Star);
                else if (double.TryParse(prefix, out value))
                    return new GridLength(value, GridUnitType.Star);
            }
            else
            {
                double value;
                if (double.TryParse(str, out value))
                    return new GridLength(value, GridUnitType.Pixel);
            }
            throw new FormatException(string.Format("Unsupported value for GridLength: {0}", str));
        }

        /// <summary>
        /// Parse lengths splited by comma
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static GridLength[] ParseAll(string str)
        {
            if (str.IsBlank())
                return new GridLength[0];

            return str.Split(',', ';')
                .Select(Parse)
                .ToArray();
        }
    }
}
