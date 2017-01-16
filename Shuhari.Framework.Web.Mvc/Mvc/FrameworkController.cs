using System;
using System.Web.Mvc;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Utils;
using Shuhari.Framework.Web.Security;

namespace Shuhari.Framework.Web.Mvc
{
    /// <summary>
    /// Base class for controller
    /// </summary>
    public abstract class FrameworkController : Controller
    {
        /// <summary>
        /// Return custom json result
        /// </summary>
        /// <param name="data"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        protected internal JsonResult CustomJson(object data, JsonRequestBehavior behavior)
        {
            return new CustomJsonResult(data, behavior);
        }

        /// <summary>
        /// Get current user
        /// </summary>
        public UserInfo CurrentUser
        {
            get { return UserManager.Instance.GetCurrentUser(HttpContext); }
        }

        /// <summary>
        /// Execute following steps in sequence:
        /// <ol>
        ///   <li>Check for ModelState, and return to origin view if any error exists;</li>
        ///   <li>Execute method specified by <paramref name="action"/></li>
        ///   <li>If action success, then set TempData.message to <paramref name="successMessage"/> 
        ///       and return <paramref name="redirect"/>;</li>
        ///   <li>If action execute failed, then call <see cref="HandleErrorAttribute"/> and return origin view;</li>
        /// </ol>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="flags"></param>
        /// <param name="model"></param>
        /// <param name="redirect"></param>
        /// <param name="successMessage"></param>
        protected internal ActionResult ExecuteAndRedirect<T>(T model, Action<T> action, 
            string successMessage, RedirectData redirect, 
            ActionExecutionFlags flags = ActionExecutionFlags.Default)
        {
            Expect.IsNotNull(model, nameof(model));
            Expect.IsNotNull(action, nameof(action));
            Expect.IsNotBlank(successMessage, nameof(successMessage));
            Expect.IsNotNull(redirect, nameof(redirect));

            if (!flags.HasFlag(ActionExecutionFlags.NoCheckModelState))
            {
                if (!ModelState.IsValid)
                    return View(model);
            }

            try
            {
                action(model);
                // successMessage = successMessage ?? GetSuccessMessage(model);
                TempData["message"] = successMessage;
                return RedirectToAction(redirect.ActionName, redirect.ControllerName, redirect.RouteValues);
            }
            catch (Exception exp)
            {
                HandleActionException(exp);
                ModelState.Clear();
                ModelState.AddModelError("", exp.Message);
                return View(model);
            }
        }

        /// <summary>
        /// Process exception
        /// </summary>
        /// <param name="exp"></param>
        protected virtual void HandleActionException(Exception exp)
        {
        }
    }
}
