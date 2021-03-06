﻿using System;
using System.Diagnostics;
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
            get { return TempData.GetTempMessage(); }
            set { TempData.SetTempMessage(value); }
        }

        /// <summary>
        /// Get current user
        /// </summary>
        public UserInfo CurrentUser => UserManager.Instance.GetCurrentUser(HttpContext);

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
                if (successMessage.IsNotBlank())
                    TempMessage = successMessage;
                return Redirect(redirectUrl);
            }
            catch (Exception exp)
            {
                HandleUiException(exp);
                return View(model);
            }
        }

        /// <summary>
        /// Process exception
        /// </summary>
        /// <param name="exp"></param>
        protected virtual void HandleActionException(Exception exp)
        {
            Debug.WriteLine(exp.GetFullTrace());
        }

        /// <summary>
        /// Handle error and set ModelState error
        /// </summary>
        /// <param name="exp"></param>
        private void HandleUiException(Exception exp)
        {
            Expect.IsNotNull(exp, nameof(exp));

            HandleActionException(exp);
            ModelState.Clear();
            ModelState.AddModelError("", exp.Message);
        }

        /// <summary>
        /// Execute action and return ResultDTO
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="action"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        protected internal JsonResult ExecuteJson<T>(T param, Action<T> action,
            ActionExecutionFlags flags = ActionExecutionFlags.Default)
        {
            var result = new ResultDto();

            if (!flags.HasFlag(ActionExecutionFlags.NoCheckModelState) &&
                !ModelState.IsValid)
            {
                result.SetResult(false, ModelState.GetFirstError().ErrorMessage);
            }
            else
            {
                try
                {
                    action(param);
                    result.SetResult(true);
                }
                catch (Exception exp)
                {
                    HandleUiException(exp);
                    result.SetResult(false, exp.Message);
                }
            }
            return CustomJson(result, JsonRequestBehavior.AllowGet);
        }
    }
}
