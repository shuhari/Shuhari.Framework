using System.Text;
using Shuhari.Framework.Text;

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
        /// Optional. Replace string in script before execute, for example changed database name
        /// </summary>
        public StringReplacer ContentReplacer { get; set; }

        /// <summary>
        /// Working directory to save script files
        /// </summary>
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// Define variable
        /// </summary>
        public string Variable { get; set; }

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
