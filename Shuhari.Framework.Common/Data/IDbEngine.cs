using System;
using System.Data;
using System.Data.Common;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Represent vendor-specifiec detail for different type of database.
    /// </summary>
    public interface IDbEngine
    {
        /// <summary>
        /// Create <see cref="ISessionFactory"/> from connection string
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        ISessionFactory CreateSessionFactory(string connectionString);

        /// <summary>
        /// Create a database connection
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateConnection();

        /// <summary>
        /// Get mapped dbType of specified CLR type
        /// </summary>
        /// <param name="clrType"></param>
        /// <returns></returns>
        DbType GetDbType(Type clrType);

        /// <summary>
        /// Create vendor-specified parameter
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        DbParameter CreateParameter(string paramName, DbType dbType, object value);
    }
}
