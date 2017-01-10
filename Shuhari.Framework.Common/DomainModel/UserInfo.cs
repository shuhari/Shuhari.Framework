using System;
using System.Collections.Generic;
using System.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.DomainModel
{
    /// <summary>
    /// Common user information saved in session, for laster access
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public UserInfo()
        {
            Properties = new Dictionary<string, string>();
        }

        /// <summary>
        /// User id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name for user
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Permissions
        /// </summary>
        public string[] Permissions { get; set; }

        /// <summary>
        /// Additional properties can be used for customizing.
        /// </summary>
        public Dictionary<string, string> Properties { get; set; }

        /// <summary>
        /// Check if user has permission
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public bool HasPermission(string permission)
        {
            Expect.IsNotBlank(permission, nameof(permission));

            return Permissions.Any(p => p.EqualsNoCase(permission));
        }
    }
}
