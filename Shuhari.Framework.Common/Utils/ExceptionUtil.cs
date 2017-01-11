using System;
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
    }
}
