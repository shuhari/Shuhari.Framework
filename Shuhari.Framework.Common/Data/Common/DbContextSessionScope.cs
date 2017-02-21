using System;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Open session when created, and dispose session when leaving using (...)
    /// </summary>
    internal class DbContextSessionScope : IDisposable
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="dbCtx"></param>
        public DbContextSessionScope(FrameworkDbContext dbCtx)
        {
            Expect.IsNotNull(dbCtx, nameof(dbCtx));

            _dbCtx = dbCtx;
            _dbCtx.OpenSession();
        }

        private readonly FrameworkDbContext _dbCtx;

        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            _dbCtx.CloseSession();
        }
    }
}
