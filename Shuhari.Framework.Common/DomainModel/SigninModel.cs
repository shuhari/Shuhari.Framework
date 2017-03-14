using System.ComponentModel.DataAnnotations;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Hold information for user sign in
    /// </summary>
    public class SigninModel
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SigninModel()
        {
        }

        /// <summary>
        /// Initialize ctor
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="rememberMe"></param>
        public SigninModel(string userName, string password, bool rememberMe)
        {
            UserName = userName;
            Password = password;
            RememberMe = rememberMe;
        }

        /// <summary>
        /// User name
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Remember user as persist
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
