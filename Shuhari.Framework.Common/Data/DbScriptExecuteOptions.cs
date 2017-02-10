using System.IO;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Database script execution options
    /// </summary>
    public class DbScriptExecuteOptions
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="workDir"></param>
        /// <param name="output"></param>
        public DbScriptExecuteOptions(string dbName, string workDir, bool output)
        {
            Expect.IsNotBlank(dbName, nameof(dbName));
            workDir = workDir ?? Directory.GetCurrentDirectory();

            this.DbName = dbName;
            this.WorkDirectory = workDir;
            this.Output = output;
        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DbName { get; private set; }

        /// <summary>
        /// 当前目录
        /// </summary>
        public string WorkDirectory { get; private set; }

        /// <summary>
        /// 是否输出控制台信息
        /// </summary>
        public bool Output { get; private set; }
    }
}
