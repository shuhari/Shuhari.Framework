using System.Collections.Generic;
using System.Linq;
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
        public UserInfo CurrentUser => UserManager.Instance.GetCurrentUser(ViewContext.HttpContext);

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

        /// <summary>
        /// Convert to select item
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public SelectListItem ToSelectItem<TId>(SelectItemDto<TId> item)
            where TId: struct
        {
            Expect.IsNotNull(item, nameof(item));

            return new SelectListItem
            {
                Text = item.Name,
                Value = item.Id.ToString(),
                Selected = item.Selected
            };
        }

        /// <summary>
        /// Convert to select item
        /// </summary>
        /// <typeparam name="TID"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public SelectListItem[] ToSelectItems<TID>(IEnumerable<SelectItemDto<TID>> items)
            where TID : struct
        {
            Expect.IsNotNull(items, nameof(items));
            return items.Select(ToSelectItem).ToArray();
        }

        /// <summary>
        /// Temp message
        /// </summary>
        protected internal string TempMessage
        {
            get { return TempData.GetTempMessage(); }
            set { TempData.SetTempMessage(value); }
        }
    }
}
