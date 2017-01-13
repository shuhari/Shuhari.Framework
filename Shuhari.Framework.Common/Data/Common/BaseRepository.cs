namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Base implementation of <see cref="IRepository"/>
    /// </summary>
    public abstract class BaseRepository : IRepository
    {
        /// <inheritdoc />
        public ISession Session { get; set; }
    }
}
