using System.Data;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Base data query
    /// </summary>
    public interface IQueryBase
    {
        /// <summary>
        /// Query belonged to session
        /// </summary>
        ISession Session { get; }

        /// <summary>
        /// Sql command
        /// </summary>
        string Sql { get; }

        /// <summary>
        /// In case value can be null, it is impossible to guess parameter type
        /// from value type, thus caller should pass <paramref name="paramType"/> manually.
        /// </summary>
        /// <param name="paramName">Parameter name</param>
        /// <param name="paramType">Parameter type</param>
        /// <param name="value">Parameter value</param>
        /// <returns>This query</returns>
        IQueryBase SetParam(string paramName, DbType paramType, object value);

        /// <summary>
        /// Wrapped method for <see cref="IDbCommand.ExecuteScalar"/>
        /// </summary>
        /// <returns></returns>
        object ExecScalar();

        /// <summary>
        /// Query like <see cref="ExecScalar"/> but return aggregation query, such as count(*)
        /// </summary>
        /// <returns></returns>
        int ExecInt();

        /// <summary>
        /// Wrapped method for <see cref="IDbCommand.ExecuteNonQuery"/>
        /// </summary>
        /// <returns></returns>
        int ExecNonQuery();
    }
}
