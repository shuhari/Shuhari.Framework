namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Select list item
    /// </summary>
    public class SelectItemDTO<TID>
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public SelectItemDTO()
        {

        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="selected"></param>
        public SelectItemDTO(TID id, string name, bool selected = false)
        {
            this.Id = id;
            this.Name = name;
            this.Selected = selected;
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
