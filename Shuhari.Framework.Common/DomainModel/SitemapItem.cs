using System.ComponentModel;
using Newtonsoft.Json;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Sitemap item
    /// </summary>
    public class SitemapItem : SitemapContainer
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public SitemapItem()
        {
            Visible = true;
        }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title"), DefaultValue(null)]
        public string Title { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [JsonProperty("url"), DefaultValue(null)]
        public string Url { get; set; }

        /// <summary>
        /// Permission
        /// </summary>
        [JsonIgnore, DefaultValue(null)]
        public string Permission { get; set; }

        /// <summary>
        /// Visible
        /// </summary>
        [ReadOnly(true), Browsable(false), JsonIgnore]
        public bool Visible { get; set; }
    }
}
