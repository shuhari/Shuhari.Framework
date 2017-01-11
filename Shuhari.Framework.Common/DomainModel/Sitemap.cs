using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Shuhari.Framework.Resources;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Sitemap root node
    /// </summary>
    public class Sitemap : SitemapContainer
    {
        /// <summary>
        /// Load from assembly resource
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static Sitemap FromResource(AssemblyResource resource)
        {
            Expect.IsNotNull(resource, nameof(resource));

            using (var ms = resource.OpenRead())
            {
                return FromStream(ms);
            }
        }

        /// <summary>
        /// Load from file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Sitemap FromFile(string filePath)
        {
            Expect.FileExist(filePath);

            using (var fs = File.OpenRead(filePath))
            {
                return FromStream(fs);
            }
        }

        /// <summary>
        /// Load from stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Sitemap FromStream(Stream stream)
        {
            Expect.IsNotNull(stream, nameof(stream));

            var sitemap = (Sitemap)XamlReader.Load(stream);
            return sitemap;
        }
    }
}
