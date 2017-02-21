using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Shuhari.Framework.DomainModel;
using Shuhari.Framework.Security;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Web.Security
{
    /// <summary>
    /// Standard Processor for user signin/signout
    /// </summary>
    public class UserManager
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="authentication"></param>
        public UserManager(IAuthentication authentication)
        {
            Expect.IsNotNull(authentication, nameof(authentication));

            _authentication = authentication;
        }

        private readonly IAuthentication _authentication;

        /// <summary>
        /// User key
        /// </summary>
        public const string USER_KEY = "__user";

        /// <summary>
        /// Get current instance
        /// </summary>
        public static UserManager Instance
        {
            get
            {
                var instance = DependencyResolver.Current.GetService<UserManager>();
                if (instance == null)
                    throw new ConfigurationErrorsException("UserManager not registered");
                return instance;
            }
        }

        /// <summary>
        /// Authenticate user. if successful then save user inforation
        /// </summary>
        /// <param name="context">http context</param>
        /// <param name="signin">singin information</param>
        public UserInfo Signin(HttpContextBase context, SigninModel signin)
        {
            Expect.IsNotNull(context, nameof(context));
            Expect.IsNotNull(signin, nameof(signin));

            var user = _authentication.Authenticate(signin);
            FormsSignin(signin);
            context.Session[USER_KEY] = user;
            return user;
        }

        /// <summary>
        /// Sign out
        /// </summary>
        /// <param name="context">http context</param>
        public void SignOut(HttpContextBase context)
        {
            Expect.IsNotNull(context, nameof(context));

            FormsSignOut();
            context.Session.Remove(USER_KEY);
        }

        /// <summary>
        /// Call <see cref="FormsAuthentication"/> to sign in, extract to ease unit test
        /// </summary>
        /// <param name="signin"></param>
        protected virtual void FormsSignin(SigninModel signin)
        {
            Expect.IsNotNull(signin, nameof(signin));

            FormsAuthentication.SetAuthCookie(signin.UserName, signin.RememberMe);
        }

        /// <summary>
        /// Call <see cref="FormsAuthentication"/> to sign out, extract to ease unit test
        /// </summary>
        protected virtual void FormsSignOut()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// Get current user, and load user data if not exist yet.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public UserInfo GetCurrentUser(HttpContextBase context)
        {
            Expect.IsNotNull(context, nameof(context));

            var user = context.Session[USER_KEY] as UserInfo;
            if (user == null)
            {
                user = _authentication.GetUser(context.User.Identity.Name);
                context.Session[USER_KEY] = user;
            }
            return user;
        }
    }
}
