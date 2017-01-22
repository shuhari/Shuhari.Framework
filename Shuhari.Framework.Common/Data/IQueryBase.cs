﻿using System.Data;
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