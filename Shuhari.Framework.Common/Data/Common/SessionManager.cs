using System.Linq;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Manage session for multiple repositories
    /// </summary>
    public static class SessionManager
    {
        /// <summary>
        /// Open session for target
        /// </summary>
        /// <param name="sessionFatory"></param>
        /// <param name="container"></param>
        public static void OpenSession(ISessionFactory sessionFatory, IRepositoryContainer container)
        {
            Expect.IsNotNull(sessionFatory, nameof(sessionFatory));
            Expect.IsNotNull(container, nameof(container));

            var repositories = container.Repositories
                .Where(r => r != null && r.Session == null)
                .ToArray();
            if (repositories.Length > 0)
            {
                var session = sessionFatory.OpenSession();
                foreach (var repos in repositories)
                    repos.Session = session;
            }
        }

        /// <summary>
        /// Close session
        /// </summary>
        /// <param name="container"></param>
        public static void CloseSession(IRepositoryContainer container)
        {
            Expect.IsNotNull(container, nameof(container));

            var repositories = container.Repositories
                .Where(r => r != null && r.Session != null)
                .ToArray();
            var sessions = repositories.Select(r => r.Session).Distinct().ToArray();
            foreach (var session in sessions)
                session.Dispose();
            foreach (var repos in repositories)
            {
                repos.Session = null;
            }
        }
    }
}
