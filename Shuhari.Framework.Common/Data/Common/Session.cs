using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Implementation of <see cref="ISession"/>
    /// </summary>
    internal class Session : ISession
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public Session(SessionFactory sessionFactory)
        {
            Expect.IsNotNull(sessionFactory, nameof(sessionFactory));

            _sessionFactory = sessionFactory;
        }

        private readonly SessionFactory _sessionFactory;

        /// <inheritdoc />
        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory; }
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }
    }
}
