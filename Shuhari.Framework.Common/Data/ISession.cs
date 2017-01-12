using System;
using System.Data;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Represent a connection to database.
    /// Caller responsible to release after use.
    /// </summary>
    public interface ISession : IDisposable
    {
        /// <summary>
        /// Owned session factory
        /// </summary>
        ISessionFactory SessionFactory { get; }

        /// <summary>
        /// Connection. It should be initialized the first time this property is visited
        /// </summary>
        IDbConnection Connection { get; }
    }
}
