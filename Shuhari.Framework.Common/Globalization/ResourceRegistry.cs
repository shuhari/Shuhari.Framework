using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Globalization
{
    /// <summary>
    /// Register and manage of <see cref="IResourceProvider"/>, 
    /// provide string getter for calling.
    /// </summary>
    public static class ResourceRegistry
    {
        /// <summary>
        /// Static initialize
        /// </summary>
        static ResourceRegistry()
        {
            _providers = new List<IResourceProvider>();
            Reset();
        }

        private static readonly List<IResourceProvider> _providers;

        /// <summary>
        /// Clear all registered providers
        /// </summary>
        public static void Reset()
        {
            _providers.Clear();
            _providers.Add(JsonResourceProvider.GetPredefined());
        }

        /// <summary>
        /// Get all providers
        /// </summary>
        public static IEnumerable<IResourceProvider> Providers => _providers.AsReadOnly();

        /// <summary>
        /// Register provider
        /// </summary>
        /// <param name="provider"></param>
        public static void Register(IResourceProvider provider)
        {
            Expect.IsNotNull(provider, nameof(provider));

            if (!_providers.Contains(provider))
                _providers.Insert(0, provider);
        }

        /// <summary>
        /// Register .Net resx format provider. See <see cref="ResxResourceProvider"/>
        /// </summary>
        /// <param name="resMgr"></param>
        public static void RegisterResx(ResourceManager resMgr)
        {
            Expect.IsNotNull(resMgr, nameof(resMgr));

            Register(new ResxResourceProvider(resMgr));
        }

        /// <summary>
        /// Get string in providers
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="culture">culture, or CurrentCulture if set to null</param>
        /// <returns>Found string, or null if not found</returns>
        public static string GetString(string key, CultureInfo culture = null)
        {
            Expect.IsNotBlank(key, nameof(key));
            culture = culture ?? Thread.CurrentThread.CurrentCulture;
            Expect.IsNotNull(culture, nameof(culture));

            foreach (var provider in _providers)
            {
                var result = provider.GetString(key, culture);
                if (result != null)
                    return result;
            }

            return null;
        }

        /// <summary>
        /// Same as <see cref="GetString(string, CultureInfo)"/>, but return a text with key
        /// to indicate which key is error. suitable for show in ui for missing hint.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string GetUiString(string key, CultureInfo culture = null)
        {
            Expect.IsNotBlank(key, nameof(key));

            var result = GetString(key, culture);
            return result ?? string.Format("?{0}?", key);
        }
    }
}
