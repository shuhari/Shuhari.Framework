using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data.Common
{
    /// <summary>
    /// Base implementation of <see cref="IDbContext"/>
    /// </summary>
    public abstract class BaseDbContext : IDbContext
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="sessionFactory"></param>
        public BaseDbContext(ISessionFactory sessionFactory)
        {
            Expect.IsNotNull(sessionFactory, nameof(sessionFactory));

            this.SessionFactory = sessionFactory;
        }

        /// <inheritdoc />
        public ISessionFactory SessionFactory { get; private set; }

        private PropertyInfo[] _repositoryProperties;

        private PropertyInfo[] GetRepositoryProperties()
        {
            if (_repositoryProperties == null)
            {
                _repositoryProperties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(IsRepositoryProperty)
                    .ToArray();
            }
            return _repositoryProperties;
        }

        private static bool IsRepositoryProperty(PropertyInfo prop)
        {
            Expect.IsNotNull(prop, nameof(prop));

            return typeof(IRepository).IsAssignableFrom(prop.PropertyType);
        }

        /// <inheritdoc />
        public IEnumerable<IRepository> Repositories
        {
            get
            {
                // Use reflection to get all repositories
                var result = new List<IRepository>();

                foreach (var prop in GetRepositoryProperties())
                {
                    var repository = prop.GetValue(this) as IRepository;
                    if (repository == null)
                        throw ExceptionBuilder.TypeAccess(FrameworkStrings.ErrorPropertyValueNull, prop.Name);
                    result.Add(repository);
                }

                return result.ToArray();
            }
        }

        /// <inheritdoc />
        public void OpenSession()
        {
            SessionManager.OpenSession(SessionFactory, this);
        }

        /// <inheritdoc />
        public void CloseSession()
        {
            SessionManager.CloseSession(SessionFactory, this);
        }

        /// <inheritdoc />
        public void ExecuteTransaction(Action action)
        {
            Expect.IsNotNull(action, nameof(action));

            var repository = Repositories.FirstOrDefault();
            if (repository == null)
                throw ExceptionBuilder.TypeAccess(FrameworkStrings.ErrorNoRepository, GetType().FullName);
            if (repository.Session == null)
                throw ExceptionBuilder.TypeAccess(FrameworkStrings.ErrorNoSession);

            using (var transaction = repository.Session.BeginTransaction())
            {
                try
                {
                    action();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
