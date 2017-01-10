using System;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Throw on <see cref="Expect"/> check failure.
    /// </summary>
    public sealed class ExpectionException : Exception
    {
        /// <summary>
        /// Initialize with message
        /// </summary>
        /// <param name="message">Error message</param>
        public ExpectionException(string message)
            : base(message)
        {
        }
    }
}