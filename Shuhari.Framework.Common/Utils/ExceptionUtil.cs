using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Shuhari.Framework.Globalization;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper methods to process exception
    /// </summary>
    public static class ExceptionUtil
    {
        /// <summary>
        /// Get full stack trace for the exception, including inner exception details.
        /// </summary>
        /// <param name="exp">Exception to trace</param>
        /// <returns>Exception trace message, includes all inner exceptions</returns>
        /// <remarks>This method can be used for logging and tracing to dump all exception details.</remarks>
        public static string GetFullTrace(this Exception exp)
        {
            Expect.IsNotNull(exp, nameof(exp));

            var sb = new StringBuilder();
            while (exp != null)
            {
                sb.AppendFormat(FrameworkStrings.ExceptionTraceFormat,
                    DateTime.Now,
                    exp.Message,
                    exp.GetType().FullName,
                    exp.StackTrace);
                exp = exp.InnerException;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Append exception detail to log file.
        /// </summary>
        /// <param name="exp">Exception to log</param>
        /// <param name="logPath">Log file path. </param>
        public static void LogToFile(this Exception exp, string logPath)
        {
            Expect.IsNotNull(exp, nameof(exp));
            Expect.IsNotBlank(logPath, nameof(logPath));

            try
            {
                File.AppendAllText(logPath, exp.GetFullTrace(), Encoding.UTF8);
            }
            catch (Exception inner)
            {
                Debug.WriteLine(inner.Message);
                Debug.WriteLine(inner.StackTrace);
            }
        }
    }
}
