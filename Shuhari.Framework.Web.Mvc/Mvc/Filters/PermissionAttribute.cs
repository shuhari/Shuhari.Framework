using System;
using System.Web.Mvc;
using Shuhari.Framework.Utils;
using Shuhari.Framework.Web.Security;

namespace Shuhari.Framework.Web.Mvc.Filters
{
    /// <summary>
    /// Specify permission
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissionAttribute : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="permission"></param>
        public PermissionAttribute(string permission)
        {
            Expect.IsNotBlank(permission, nameof(permission));

            _permission = permission;
        }

        private readonly string _permission;

        /// <inheritdoc />
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userManager = DependencyResolver.Current.GetService<UserManager>();
            Expect.IsNotNull(userManager, nameof(userManager));

            var httpCtx = filterContext.HttpContext;
            var user = userManager.GetCurrentUser(filterContext.HttpContext);
            if (user == null)
                throw new ApplicationException("Could not found current user");
            if (!user.HasPermission(_permission))
                filterContext.Result = new HttpStatusCodeResult(403, "Forbidden");
        }

        /// <inheritdoc />
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}
