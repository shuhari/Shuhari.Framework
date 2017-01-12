using System.Data;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Factory class to store database information, such as connection string
    /// and entity mappings, and create instance of <see cref="ISession"/>.
    /// </summary>
    public interface ISessionFactory
    {
        /// <summary>
        /// Database engine
        /// </summary>
        IDbEngine Engine { get; }

        /// <summary>
        /// Open session instance
        /// </summary>
        /// <param name="parameters">Additional parametes to create session.
        /// for example, switch connection in read-write-separated scene.
        /// Application with only one database can ignore this parameter.</param>
        /// <returns>Opened session</returns>
        /// <remarks>Caller is responsible for release session after use it.</remarks>
        ISession OpenSession(object parameters = null);

        /// <summary>
        /// Create a data connection, set connection string and open
        /// </summary>
        /// <param name="parameters">Parameters when creating session object</param>
        /// <returns></returns>
        IDbConnection OpenConnection(object parameters = null);
    }
}
