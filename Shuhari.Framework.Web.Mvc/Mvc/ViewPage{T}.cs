using System.Web.Mvc;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;
using Shuhari.Framework.Web.Security;

namespace Shuhari.Framework.Web.Mvc
{
    /// <summary>
    /// Base view page
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ViewPage<T> : WebViewPage<T>
    {
        /// <summary>
        /// Set title throw ViewBag.Title
        /// </summary>
        public string PageTitle
        {
            get { return ViewBag.Title; }
            set { ViewBag.Title = value; }
        }

        /// <summary>
        /// Get current user
        /// </summary>
        public UserInfo CurrentUser
        {
            get { return UserManager.Instance.GetCurrentUser(ViewContext.HttpContext); }
        }

        /// <summary>
        /// Get resource string. See <see cref="ResourceRegistry"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ResString(string key)
        {
            Expect.IsNotBlank(key, nameof(key));

            return ResourceRegistry.GetUiString(key);
        }

        /*/// <summary>
        /// Entity descriptor
        /// </summary>
        protected IEntityDescriptor EntityDescriptor
        {
            get { return DependencyResolver.Current.GetService<IEntityDescriptor>(); }
        }

        /// <summary>
        /// Get action title
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetActionTitle(object obj = null)
        {
            string action = Convert.ToString(ViewContext.RouteData.Values["action"]);
            return EntityDescriptor.GetActionTitle(action, obj);
        }

        /// <summary>
        /// Get delete prompt
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetDeletePrompt(object obj)
        {
            return EntityDescriptor.GetDeletePrompt(obj);
        }*/
    }
}
