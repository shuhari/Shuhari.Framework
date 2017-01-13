using System;
using System.Data;
using System.Data.Common;
using Shuhari.Framework.Resources;
using Shuhari.Framework.Text;

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
        /// Get type name for specified CLR type, 
        /// used in case such as include primary key in query.
        /// Currently only int/long/guid are required.
        /// </summary>
        /// <param name="clrType"></param>
        /// <returns></returns>
        string GetDbTypeName(Type clrType);

        /// <summary>
        /// Create vendor-specified parameter
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        DbParameter CreateParameter(string paramName, DbType dbType, object value);

        /// <summary>
        /// Execute management command
        /// </summary>
        /// <param name="options">command options</param>
        /// <returns>execute output</returns>
        string ExecuteCommand(DbManagementCommandOptions options);

        /// <summary>
        /// Execute script compiled as assembly resource
        /// </summary>
        /// <param name="resource">script resource</param>
        /// <param name="options">Command options, or null if no options required</param>
        /// <param name="replacer">Optional replace one string in script. Useful for test when actual database name
        /// should changed to test database name</param>
        /// <returns></returns>
        string ExecuteResourceScript(AssemblyResource resource, DbManagementCommandOptions options = null,
            StringReplacer replacer = null);

        /// <summary>
        /// Create query builder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryBuilder<T> CreateQueryBuilder<T>() where T : class, new();
    }
}
