namespace Shuhari.Framework.Web.Mvc
{
    /// <summary>
    /// Redirect data
    /// </summary>
    public class RedirectData
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="routeValues"></param>
        public RedirectData(string controller, string action, object routeValues = null)
        {
            this.ControllerName = controller;
            this.ActionName = action;
            this.RouteValues = routeValues;
        }

        /// <summary>
        /// Controller name
        /// </summary>
        public string ControllerName { get; private set; }

        /// <summary>
        /// Action name
        /// </summary>
        public string ActionName { get; private set; }

        /// <summary>
        /// Route values
        /// </summary>
        public object RouteValues { get; private set; }
    }
}
