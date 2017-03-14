using NUnit.Framework;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Template base class for database test. Logic:
    /// <ul>
    ///   <header>If mode set to read-only:</header>
    ///   <li>Create database on OneTimeSetUp;</li>
    ///   <li>Drop database on OneTimeTearDown;</li>
    /// </ul>
    /// <ul>
    ///   <header>If mode set to read-write:</header>
    ///   <li>Create database on SetUp;</li>
    ///   <li>Drop database on Down;</li>
    /// </ul>
    /// And in any case, session will be opened on SetUp, and closed on TearDown
    /// </summary>
    public abstract class DbTestBase
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="readOnly">Read-only mode</param>
        protected DbTestBase(bool readOnly)
        {
            ReadOnly = readOnly;
        }

        /// <summary>
        /// If in read-only mode
        /// </summary>
        public bool ReadOnly { get; }

        /// <summary>
        /// Session
        /// </summary>
        public ISession Session { get; private set; }

        /// <summary>
        /// Implement create database logic.
        /// </summary>
        protected internal abstract void CreateDatabase();

        /// <summary>
        /// Implement drop database logic.
        /// </summary>
        protected internal abstract void DropDatabase();

        /// <summary>
        /// Implement open session logic
        /// </summary>
        /// <returns></returns>
        protected internal abstract ISession OpenSession();

        /// <summary>
        /// Custom logic executed after setup
        /// </summary>
        protected internal virtual void AfterSetUp()
        {
        }

        /// <summary>
        /// Perform one time setup
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            if (ReadOnly)
            {
                CreateDatabase();
            }
        }

        /// <summary>
        /// Perform one time teardown
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (ReadOnly)
            {
                DropDatabase();
            }
        }

        /// <summary>
        /// Setup
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            if (!ReadOnly)
            {
                CreateDatabase();
            }

            Session = OpenSession();
            AfterSetUp();
        }

        /// <summary>
        /// Tear down
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            if (Session != null)
            {
                Session.Dispose();
                Session = null;
            }

            if (!ReadOnly)
            {
                DropDatabase();
            }
        }
    }
}
