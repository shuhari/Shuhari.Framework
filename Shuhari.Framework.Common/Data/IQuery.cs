using System.Data;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Represent data query
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// Query belonged to session
        /// </summary>
        ISession Session { get; }

        /// <summary>
        /// Set query parameter. Parameter type are guessed from value type, 
        /// so <strong>value could not be null</strong>.
        /// </summary>
        /// <param name="paramName">Parameter name</param>
        /// <param name="value"></param>
        /// <returns>This query</returns>
        void Set(string paramName, object value);

        /// <summary>
        /// In case value can be null, it is impossible to guess parameter type
        /// from value type, thus caller should pass <paramref name="paramType"/> manually.
        /// </summary>
        /// <param name="paramName">Parameter name</param>
        /// <param name="paramType">Parameter type</param>
        /// <param name="value">Parameter value</param>
        /// <returns>This query</returns>
        void Set(string paramName, DbType paramType, object value);

        /// <summary>
        /// Set query parameter
        /// </summary>
        /// <param name="q">Query parameter</param>
        /// <returns>This query</returns>
        void SetPaginiation(QueryDTO q);

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
