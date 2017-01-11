using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Resources;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper methods to process byte array
    /// </summary>
    public static class ByteArrayUtil
    {
        /// <summary>
        /// Convert bytes content to hex string
        /// </summary>
        /// <param name="data">Bytes data</param>
        /// <param name="upperCase">If result in upper case</param>
        /// <returns>Hex string</returns>
        public static string ToHex(this byte[] data, bool upperCase = false)
        {
            Expect.IsNotNull(data, nameof(data));

            string format = upperCase ? "X2" : "x2";
            return string.Join("", data.Select(b => b.ToString(format)));
        }

        /// <summary>
        /// Given string in hex format (<see cref="ToHex(byte[], bool)"/>),
        /// convert back to bytes
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] Parse(string str)
        {
            Expect.IsNotNull(str, nameof(str));

            if (str.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
                str = str.Substring(2);
            Expect.That(str.Length % 2 == 0, () => new FormatException(FrameworkStrings.ErrorBytesFormat));
            var bytes = new List<byte>();
            for (int i = 0; i < str.Length / 2; i++)
                bytes.Add(byte.Parse(str.Substring(i * 2, 2), NumberStyles.AllowHexSpecifier));
            return bytes.ToArray();
        }
    }
}
