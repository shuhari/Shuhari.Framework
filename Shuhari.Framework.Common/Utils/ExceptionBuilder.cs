using System;
using Shuhari.Framework.Resources;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Helper to build exception
    /// </summary>
    public static class ExceptionBuilder
    {
        /// <summary>
        /// Create <see cref="ExpectionException"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ExpectionException Expection(string format, params object[] args)
        {
            return new ExpectionException(string.Format(format, args));
        }

        /// <summary>
        /// Create <see cref="ResourceException"/>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ResourceException Resource(string format, params object[] args)
        {
            return new ResourceException(string.Format(format, args));
        }

        /// <summary>
        /// Create NotSupportedException
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static NotSupportedException NotSupported(string format, params object[] args)
        {
            return new NotSupportedException(string.Format(format, args));
        }
    }
}