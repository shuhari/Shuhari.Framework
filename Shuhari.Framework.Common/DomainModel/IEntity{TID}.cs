namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Entity interface with primary key
    /// </summary>
    /// <typeparam name="TID"></typeparam>
    public interface IEntity<TID>
        where TID: struct
    {
        /// <summary>
        /// Primary key always named Id
        /// </summary>
        TID Id { get; set; }
    }
}
