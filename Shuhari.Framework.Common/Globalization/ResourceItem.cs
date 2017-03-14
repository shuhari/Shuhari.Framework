using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Globalization
{
    /// <summary>
    /// Define one resource entry in multiple languages
    /// </summary>
    internal class ResourceItem
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public ResourceItem()
        {
            _items = new Dictionary<string, string>();
        }

        private readonly Dictionary<string, string> _items;

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="prop"></param>
        internal void Load(JProperty prop)
        {
            Expect.IsNotNull(prop, nameof(prop));

            foreach (var child in prop.Children())
            {
                foreach (JProperty cultureItem in child.Children())
                {
                    string cultureName = NormalizeCultureName(cultureItem.Name);
                    _items[cultureName] = (string)cultureItem.Value;
                }
            }
        }

        /// <summary>
        /// Name normalized to lowercase
        /// </summary>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        private string NormalizeCultureName(string cultureName)
        {
            Expect.IsNotBlank(cultureName, nameof(cultureName));
            return cultureName.ToLowerInvariant();
        }

        /// <summary>
        /// Get string, if not exist then look in fallback
        /// </summary>
        /// <param name="cultureName"></param>
        /// <param name="fallbackCultureName"></param>
        public string GetString(string cultureName, string fallbackCultureName)
        {
            Expect.IsNotBlank(cultureName, nameof(cultureName));

            cultureName = NormalizeCultureName(cultureName);
            if (_items.ContainsKey(cultureName))
                return _items[cultureName];

            if (fallbackCultureName.IsNotBlank())
            {
                fallbackCultureName = NormalizeCultureName(fallbackCultureName);
                if (_items.ContainsKey(fallbackCultureName))
                    return _items[fallbackCultureName];
            }
            return null;
        }
    }
}
