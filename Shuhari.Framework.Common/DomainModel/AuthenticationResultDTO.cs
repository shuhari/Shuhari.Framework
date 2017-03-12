namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Save authentication result
    /// </summary>
    public class AuthenticationResultDto : ResultDto
    {
        /// <summary>
        /// User information, or null if auth failed.
        /// </summary>
        public UserInfo User { get; set; }
    }
}
