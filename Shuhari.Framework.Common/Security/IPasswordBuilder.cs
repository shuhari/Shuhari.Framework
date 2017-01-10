namespace Shuhari.Framework.Security
{
    /// <summary>
    /// Describe how to generate and check password
    /// </summary>
    public interface IPasswordBuilder
    {
        /// <summary>
        /// Given password in plain text, create hash/salt pair
        /// </summary>
        /// <param name="password">input password</param>
        /// <returns>Generated pair, should store for later check</returns>
        PasswordPair Generate(string password);

        /// <summary>
        /// Check if password is correct
        /// </summary>
        /// <param name="password">Input password</param>
        /// <param name="pair">Stored hash/salt pair</param>
        /// <returns></returns>
        bool IsCorrect(string password, PasswordPair pair);
    }
}
