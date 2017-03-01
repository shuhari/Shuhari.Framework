using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Web
{
    /// <summary>
    /// Mock http context
    /// </summary>
    public class MockHttpContext : HttpContextBase
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public MockHttpContext()
        {
            _session = new MockHttpSession();

            SetUserAnonymous();
        }

        private readonly MockHttpSession _session;

        /// <inheritdoc />
        public override HttpSessionStateBase Session
        {
            get { return _session; }
        }

        /// <inheritdoc />
        public override IPrincipal User { get; set; }

        /// <summary>
        /// Set user to anonymouse (not authenticated)
        /// </summary>
        public MockHttpContext SetUserAnonymous()
        {
            User = new GenericPrincipal(new GenericIdentity(""), new string[0]);
            return this;
        }

        /// <summary>
        /// Set user to authenticated
        /// </summary>
        /// <param name="name"></param>
        public MockHttpContext SetUser(string name)
        {
            Expect.IsNotBlank(name, nameof(name));

            User = new GenericPrincipal(new GenericIdentity(name), new string[0]);
            return this;
        }

        /// <summary>
        /// Create controller context
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public ControllerContext CreateControllerContext(Controller controller)
        {
            Expect.IsNotNull(controller, nameof(controller));

            var requestCtx = new RequestContext(this, new RouteData());
            var ctx = new ControllerContext(requestCtx, controller);
            return ctx;
        }

        /// <summary>
        /// Create view context
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public ViewContext CreateViewContext(Controller controller, IView view)
        {
            Expect.IsNotNull(controller, nameof(controller));
            Expect.IsNotNull(view, nameof(view));

            var controllerCtx = CreateControllerContext(controller);
            var viewData = new ViewDataDictionary();
            var tempData = new TempDataDictionary();
            var writer = new StringWriter();
            var viewCtx = new ViewContext(controllerCtx, view, viewData, tempData, writer);
            return viewCtx;
        }
    }
}
