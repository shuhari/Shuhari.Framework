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

        private const string KEY_TEMP_MSG = "__message";

        /// <summary>
        /// Temp message
        /// </summary>
        protected internal string TempMessage
        {
            get
            {
                return TempData.ContainsKey(KEY_TEMP_MSG) ? (string)TempData[KEY_TEMP_MSG] : null;
            }
            set
            {
                TempData[KEY_TEMP_MSG] = value;
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
        protected internal JsonResult ExecuteAjax<T>(T model, Action<T> action,
            string successMessage, string redirectUrl = null,
            ActionExecutionFlags flags = ActionExecutionFlags.Default)
        {
            Expect.IsNotNull(model, nameof(model));
            Expect.IsNotNull(action, nameof(action));
            Expect.IsNotBlank(successMessage, nameof(successMessage));

            var result = new ValidationResultDTO();
            result.RedirectUrl = redirectUrl;
            if (!flags.HasFlag(ActionExecutionFlags.NoCheckModelState))
            {
                if (!ModelState.IsValid)
                {
                    SetValidationErrors(result);
                    return CustomJson(result, JsonRequestBehavior.AllowGet);
                }
            }

            try
            {
                action(model);
                TempMessage = successMessage;
            }
            catch (Exception exp)
            {
                HandleActionException(exp);
                result.SetError("", exp.Message);
            }
            return CustomJson(result, JsonRequestBehavior.AllowGet);
        }

        private void SetValidationErrors(ValidationResultDTO result)
        {
            Expect.IsNotNull(result, nameof(result));

            var errors = new List<ValidationErrorDTO>();

            foreach (var key in ModelState.Keys)
            {
                var stateItem = ModelState[key];
                errors.AddRange(stateItem.Errors
                    .Select(x => new ValidationErrorDTO(key, x.ErrorMessage)));
            }
            result.SetErrors(errors);
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
