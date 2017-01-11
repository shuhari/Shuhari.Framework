using System;

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
    }
}
