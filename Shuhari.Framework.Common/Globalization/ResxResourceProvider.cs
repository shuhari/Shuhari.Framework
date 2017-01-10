using System.Globalization;
using System.Resources;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Globalization
{
    /// <summary>
    /// Based on .Net resx file
    /// </summary>
    public class ResxResourceProvider : IResourceProvider
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="resMgr"></param>
        public ResxResourceProvider(ResourceManager resMgr)
        {
            Expect.IsNotNull(resMgr, nameof(resMgr));

            _resManager = resMgr;
        }

        private readonly ResourceManager _resManager;

        /// <inheritdoc />
        public string GetString(string key, CultureInfo culture)
        {
            return _resManager.GetString(key, culture);
        }
    }
}
