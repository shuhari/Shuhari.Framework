namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Repository interface
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// get or set session
        /// </summary>
        ISession Session { get; set; }
    }
}
