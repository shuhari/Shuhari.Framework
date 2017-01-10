using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shuhari.Framework.Resources;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Globalization
{
    /// <summary>
    /// Json based resources
    /// </summary>
    public class JsonResourceProvider : IResourceProvider
    {
        /// <summary>
        /// Get predefined provider
        /// </summary>
        /// <returns></returns>
        public static JsonResourceProvider GetPredefined()
        {
            const string RESOURCE_PATH = "ResourceFiles/framework-strings.json";
            return FromResource(typeof(JsonResourceProvider).Assembly.GetResource(RESOURCE_PATH));
        }

        /// <summary>
        /// Load from resource
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static JsonResourceProvider FromResource(AssemblyResource resource)
        {
            Expect.IsNotNull(resource, nameof(resource));

            using (var stream = resource.OpenRead())
            {
                return FromStream(stream);
            }
        }

        /// <summary>
        /// Load from stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static JsonResourceProvider FromStream(Stream stream)
        {
            Expect.IsNotNull(stream, nameof(stream));

            var text = EncodingUtil.DefaultEncoding.GetString(stream.ReadToEnd());
            var root = (JObject)JsonConvert.DeserializeObject(text);
            var provider = new JsonResourceProvider();
            provider.Load(root);
            return provider;
        }

        /// <summary>
        /// Initialize
        /// </summary>
        private JsonResourceProvider()
        {
            _items = new Dictionary<string, ResourceItem>();
        }

        private readonly Dictionary<string, ResourceItem> _items;

        /// <summary>
        /// Load from json
        /// </summary>
        /// <param name="json"></param>
        private void Load(JObject json)
        {
            Expect.IsNotNull(json, nameof(json));

            _items.Clear();
            foreach (JProperty prop in json.Children())
            {
                var item = new ResourceItem();
                item.Load(prop);
                _items[prop.Name] = item;
            }
        }

        /// <summary>
        /// Get string
        /// </summary>
        /// <param name="key"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public string GetString(string key, CultureInfo culture)
        {
            Expect.IsNotBlank(key, nameof(key));
            Expect.IsNotNull(culture, nameof(culture));
            const string FALLBACK_CULTURE = "en";

            if (_items.ContainsKey(key))
            {
                return _items[key].GetString(culture.Name, FALLBACK_CULTURE);
            }
            return null;
        }
    }
}
