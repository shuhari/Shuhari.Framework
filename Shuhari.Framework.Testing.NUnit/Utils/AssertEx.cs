using System;
using NUnit.Framework;

namespace Shuhari.Framework.Utils
{
    /// <summary>
    /// Additional assert methods
    /// </summary>
    public static class AssertEx
    {
        /// <summary>
        /// Assert guid is not empty
        /// </summary>
        /// <param name="guid">Guid to check</param>
        /// <exception cref="AssertionException">Throw if parameter is empty</exception>
        public static void IsNotEmpty(Guid guid)
        {
            if (guid == Guid.Empty)
                throw new AssertionException("Expected: <not empty guid>, But got: <empty>");
        }
    }
}
