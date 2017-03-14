using System.Linq;
using System.Text;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Security
{
    /// <summary>
    /// Template class to generic and verify password pair.
    /// Generate algorithm are delegated to derived class.
    /// </summary>
    public abstract class BasePasswordBuilder : IPasswordBuilder
    {
        /// <summary>
        /// Generate a random salt
        /// </summary>
        /// <returns></returns>
        protected abstract byte[] GenerateSalt();

        /// <summary>
        /// Given password and salt, compute the hash
        /// </summary>
        /// <param name="password">Password in plain text</param>
        /// <param name="salt">Password salt</param>
        /// <returns>Password hash</returns>
        protected abstract byte[] ComputeHash(string password, byte[] salt);

        /// <summary>
        /// Commonly use utf-8 to encode string, can be overrided by derived class
        /// </summary>
        protected virtual Encoding Encoding => Encoding.UTF8;

        /// <summary>
        /// Generate password salt/hash pair
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public PasswordPair Generate(string password)
        {
            Expect.IsNotNull(password, nameof(password));

            var salt = GenerateSalt();
            var hash = ComputeHash(password, salt);
            return new PasswordPair(hash, salt);
        }

        /// <summary>
        /// Check if given password match encrypted form.
        /// </summary>
        /// <param name="password">Given password</param>
        /// <param name="pair">Encrypt password pair</param>
        /// <returns></returns>
        public bool IsCorrect(string password, PasswordPair pair)
        {
            Expect.IsNotNull(password, nameof(password));
            Expect.IsNotNull(pair, nameof(pair));

            var inputHash = ComputeHash(password, pair.Salt);
            return inputHash.SequenceEqual(pair.Hash);
        }
    }
}
