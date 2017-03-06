using System.Web;
using System.Web.Mvc;

namespace Shuhari.Framework.Web.Mvc
{
    /// <summary>
    /// Add helper methods for TagBuilder
    /// </summary>
    public static class TagExtensions
    {
        /// <summary>
        /// Add css class
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static TagBuilder AddClass(this TagBuilder tag, string className)
        {
            tag.AddCssClass(className);
            return tag;
        }

        /// <summary>
        /// html result
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static IHtmlString ToHtml(this TagBuilder tag)
        {
            return new HtmlString(tag.ToString());
        }

        /// <summary>
        /// Set Html content
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static TagBuilder Html(this TagBuilder tag, string content)
        {
            tag.InnerHtml = content;
            return tag;
        }

        /// <summary>
        /// Append html content
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static TagBuilder AppendHtml(this TagBuilder tag, string content)
        {
            tag.InnerHtml += content;
            return tag;
        }

        /// <summary>
        /// Set attribute
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TagBuilder Attr(this TagBuilder tag, string name, string value)
        {
            tag.MergeAttribute(name, value);
            return tag;
        }
    }
}
