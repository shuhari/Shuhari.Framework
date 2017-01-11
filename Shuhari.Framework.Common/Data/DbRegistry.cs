using System;
using System.Collections.Generic;
using Shuhari.Framework.Globalization;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Register and manage instance of <see cref="IDbEngine"/>.
    /// </summary>
    public static class DbRegistry
    {
        /// <summary>
        /// Register known engines
        /// </summary>
        static DbRegistry()
        {
            _engines = new Dictionary<DatabaseType, IDbEngine>();
            Register(DatabaseType.SqlServer, new SqlServer.SqlDbEngine());
        }

        private static readonly Dictionary<DatabaseType, IDbEngine> _engines;

        /// <summary>
        /// Register engine
        /// </summary>
        /// <param name="type"></param>
        /// <param name="engine"></param>
        public static void Register(DatabaseType type, IDbEngine engine)
        {
            Expect.IsNotNull(engine, nameof(engine));

            _engines[type] = engine;
        }

        /// <summary>
        /// Get engine by type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IDbEngine GetEngine(DatabaseType type)
        {
            if (!_engines.ContainsKey(type))
                throw ExceptionBuilder.NotSupported(FrameworkStrings.ErrorUnknownDbType, type);
            return _engines[type];
        }
    }
}
