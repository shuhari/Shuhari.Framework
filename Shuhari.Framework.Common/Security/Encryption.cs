using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Security
{
    /// <summary>
    /// Common hash (encryption) methods
    /// </summary>
    public static class Encryption
    {
        /// <summary>
        /// Compute MD5 hash result
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] MD5(this byte[] data)
        {
            Expect.IsNotNull(data, nameof(data));

            using (var md5 = new MD5CryptoServiceProvider())
            {
                return md5.ComputeHash(data);
            }
        }

        /// <summary>
        /// Compute SHA256 result
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] SHA256(this byte[] data)
        {
            Expect.IsNotNull(data, nameof(data));

            using (var sha = new SHA256Managed())
            {
                return sha.ComputeHash(data);
            }
        }
    }
}
