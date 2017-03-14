namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Test of repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryTestBase<T> : DbTestBase
        where T : IRepository, new()
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="readOnly"></param>
        protected RepositoryTestBase(bool readOnly)
            : base(readOnly)
        {
        }

        /// <summary>
        /// Repository
        /// </summary>
        public T Repository { get; private set; }

        /// <summary>
        /// Create repository
        /// </summary>
        protected internal override void AfterSetUp()
        {
            Repository = new T {Session = Session};
        }
    }
}
