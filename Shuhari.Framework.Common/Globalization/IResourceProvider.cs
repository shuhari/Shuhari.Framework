using System.Globalization;

namespace Shuhari.Framework.Globalization
{
    /// <summary>
    /// Provider for resource strings in different format, 
    /// such as internal(json) and .resx
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// Get string by key
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        string GetString(string key, CultureInfo culture);
    }
}
