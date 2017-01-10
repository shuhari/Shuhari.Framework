using System;

namespace Shuhari.Framework.Resources
{
    /// <summary>
    /// Thrown when resource related issue occured
    /// </summary>
    public sealed class ResourceException : Exception
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="message">Error message</param>
        public ResourceException(string message)
            : base(message)
        {
        }
    }
}