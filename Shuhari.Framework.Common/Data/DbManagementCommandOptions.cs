using System.Text;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Options to execute database command
    /// </summary>
    public class DbManagementCommandOptions
    {
        /// <summary>
        /// Script file name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// File encoding, or null if use default
        /// </summary>
        public Encoding FileEncoding { get; set; }

        /// <summary>
        /// Get default options
        /// </summary>
        /// <returns></returns>
        public static DbManagementCommandOptions GetDefault()
        {
            return new DbManagementCommandOptions
            {
            };
        }
    }
}
