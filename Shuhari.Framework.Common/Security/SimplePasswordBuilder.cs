using System;
using System.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Security
{
    /// <summary>
    /// A basic password generator that use following rules:
    /// <ul>
    ///   <li>salt = new guid()</li>
    ///   <li>hash = SHA256(password + salt)</li>
    /// </ul>
    /// </summary>
    public sealed class SimplePasswordBuilder : BasePasswordBuilder
    {
        /// <inheritdoc />
        public override byte[] GenerateSalt()
        {
            return Guid.NewGuid().ToByteArray();
        }

        /// <inheritdoc />
        public override byte[] ComputeHash(string password, byte[] salt)
        {
            Expect.IsNotNull(password, nameof(password));
            Expect.IsNotNull(salt, nameof(salt));

            var allBytes = Encoding.GetBytes(password).Concat(salt).ToArray();
            return Encryption.SHA256(allBytes);
        }
    }
}
