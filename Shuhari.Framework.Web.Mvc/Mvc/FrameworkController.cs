using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Temp message
        /// </summary>
        protected internal string TempMessage
        {
            get
            {
                return TempData.ContainsKey(MvcConstants.KEY_TEMP_MSG) ? 
                    (string)TempData[MvcConstants.KEY_TEMP_MSG] : null;
            }
            set
            {
                TempData[MvcConstants.KEY_TEMP_MSG] = value;
            }
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
        ///       and return <paramref name="redirectUrl"/>;</li>
        ///   <li>If action execute failed, then call <see cref="HandleErrorAttribute"/> and return origin view;</li>
        /// </ol>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="flags"></param>
        /// <param name="model"></param>
        /// <param name="redirectUrl"></param>
        /// <param name="successMessage"></param>
        protected internal ActionResult ExecuteAndRedirect<T>(T model, Action<T> action,
            string redirectUrl, string successMessage,
            ActionExecutionFlags flags = ActionExecutionFlags.Default)
        {
            Expect.IsNotNull(model, nameof(model));
            Expect.IsNotNull(action, nameof(action));
            Expect.IsNotBlank(redirectUrl, nameof(redirectUrl));

            if (!flags.HasFlag(ActionExecutionFlags.NoCheckModelState))
            {
                if (!ModelState.IsValid)
                    return View(model);
            }

            try
            {
                action(model);
                // successMessage = successMessage ?? GetSuccessMessage(model);
                if (successMessage.IsNotBlank())
                    TempMessage = successMessage;
                return Redirect(redirectUrl);
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

        /// <summary>
        /// Execute action and return ResultDTO
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected internal JsonResult ExecuteJsonResult<T>(T param, Action<T> action)
        {
            var result = new ResultDTO();
            try
            {
                action(param);
                result.SetResult(true);
            }
            catch (Exception exp)
            {
                HandleActionException(exp);
                result.SetResult(false, exp.Message);
            }
            return CustomJson(result, JsonRequestBehavior.AllowGet);
        }
    }
}
