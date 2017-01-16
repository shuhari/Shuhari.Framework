using System;

namespace Shuhari.Framework.Web.Mvc
{
    /// <summary>
    /// Action flags
    /// </summary>
    [Flags]
    public enum ActionExecutionFlags
    {
        /// <summary>
        /// Default
        /// </summary>
        Default = 0,

        /// <summary>
        /// Skip ModelState check
        /// </summary>
        NoCheckModelState = 0x1,
    }
}
