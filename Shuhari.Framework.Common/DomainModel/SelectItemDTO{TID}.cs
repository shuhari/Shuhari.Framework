namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Select list item
    /// </summary>
    public class SelectItemDto<TID>
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public SelectItemDto()
        {

        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="selected"></param>
        public SelectItemDto(TID id, string name, bool selected = false)
        {
            Id = id;
            Name = name;
            Selected = selected;
        }

        /// <summary>
        /// Id
        /// </summary>
        public TID Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Selected
        /// </summary>
        public bool Selected { get; set; }
    }
}
