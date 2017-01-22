using System;
using System.IO;
using NUnit.Framework;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.UnitTests.Utils
{
    [TestFixture]
    public class ExceptionUtilTest
    {
        [Test]
        public void GetFullTrace()
        {
            const string outerMsg = "Outer error";
            const string innerMsg = "Inner error";
            string trace;

            try
            {
                throw new ApplicationException(outerMsg);
            }
            catch (Exception outer)
            {
                try
                {
                    throw new ArgumentException(innerMsg, outer);
                }
                catch (Exception inner)
                {
                    trace = inner.GetFullTrace();
                }
            }

            StringAssert.Contains(outerMsg, trace);
            StringAssert.Contains(innerMsg, trace);
        }

        [Test]
        public void LogToFile()
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "exception.log");
            File.Delete(logPath);
            const string MSG = "test error";

            try
            {
                throw new ApplicationException(MSG);
            }
            catch(Exception exp)
            {
                exp.LogToFile(logPath);
            }

            StringAssert.Contains(MSG, File.ReadAllText(logPath));
        }
    }
}