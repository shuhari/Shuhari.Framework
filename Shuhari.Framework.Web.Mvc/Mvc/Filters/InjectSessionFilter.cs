using System.Web.Mvc;
using Shuhari.Framework.Data;
using Shuhari.Framework.Data.Common;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Web.Mvc.Filters
{
    /// <summary>
    /// Inject session
    /// </summary>
    public class InjectSessionFilter : IActionFilter
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="sessionFactory"></param>
        public InjectSessionFilter(ISessionFactory sessionFactory)
        {
            Expect.IsNotNull(sessionFactory, nameof(sessionFactory));

            _sessionFactory = sessionFactory;
        }

        private readonly ISessionFactory _sessionFactory;

        /// <inheritdoc />
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var container = filterContext.Controller as IRepositoryContainer;
            if (container != null)
                SessionManager.OpenSession(_sessionFactory, container);
        }

        /// <inheritdoc />
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var container = filterContext.Controller as IRepositoryContainer;
            if (container != null)
                SessionManager.CloseSession(container);
        }
    }
}
