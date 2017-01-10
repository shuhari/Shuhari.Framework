using System;
using System.Text;
using Shuhari.Framework.Resources;

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
            var crlf = Environment.NewLine;
            while (exp != null)
            {
                sb.AppendFormat(FrameworkStrings.EXCEPTION_TRACE_FORMAT,
                    crlf,
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
