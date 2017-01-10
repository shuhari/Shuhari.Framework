using System.Security.Authentication;
using Shuhari.Framework.DomainModel;

namespace Shuhari.Framework.Security
{
    /// <summary>
    /// Abstract the authentication logic, which should be implemented by application
    /// </summary>
    public interface IAuthentication
    {
        /// <summary>
        /// Authenticate user login.
        /// </summary>
        /// <param name="signin">Signin information</param>
        /// <returns>User data if authenticate success</returns>
        /// <exception cref="AuthenticationException">Throw if authenticate failed.</exception>
        UserInfo Authenticate(SigninModel signin);

        /// <summary>
        /// If user is signed in using persist cookie (thus skip the signin process),
        /// This method responsible for get user data.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        UserInfo GetUser(string userName);
    }
}
