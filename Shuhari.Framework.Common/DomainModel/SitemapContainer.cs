using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;
using Newtonsoft.Json;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Base class for Sitemap
    /// </summary>
    [ContentProperty("Children")]
    public abstract class SitemapContainer
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public SitemapContainer()
        {
            Children = new List<SitemapItem>();
        }

        /// <summary>
        /// Child nodes
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [JsonProperty("children")]
        public List<SitemapItem> Children { get; private set; }
    }
}
