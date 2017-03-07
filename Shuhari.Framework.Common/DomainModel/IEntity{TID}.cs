namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Entity interface with primary key
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IEntity<TId>
        where TId: struct
    {
        /// <summary>
        /// Primary key always named Id
        /// </summary>
        TId Id { get; set; }
    }
}
