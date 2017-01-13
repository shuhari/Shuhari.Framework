using System.Collections.Generic;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Manage multiple repository as one unit, so they can share same session/transaction
    /// </summary>
    public interface IRepositoryContainer
    {
        /// <summary>
        /// Get all repositories in this container
        /// </summary>
        IEnumerable<IRepository> Repositories { get; }
    }
}
