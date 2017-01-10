using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Security
{
    /// <summary>
    /// Save password salt and hash in pair
    /// </summary>
    public sealed class PasswordPair
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="salt"></param>
        public PasswordPair(byte[] hash, byte[] salt)
        {
            Expect.IsNotNull(hash, nameof(hash));
            Expect.IsNotNull(salt, nameof(salt));

            this.Hash = hash;
            this.Salt = salt;
        }

        /// <summary>
        /// Password hash
        /// </summary>
        public byte[] Hash { get; private set; }

        /// <summary>
        /// Password salt
        /// </summary>
        public byte[] Salt { get; private set; }
    }
}
