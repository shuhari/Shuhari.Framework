using System.Collections.Generic;
using System.Diagnostics;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.IO
{
    /// <summary>
    /// Execute shell command
    /// </summary>
    public class CommandLine
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="cmd"></param>
        public CommandLine(string cmd)
        {
            Expect.IsNotBlank(cmd, nameof(cmd));

            _cmd = cmd;
            _args = new List<string>();
        }

        private readonly string _cmd;

        private string _workDir;

        private readonly List<string> _args;

        /// <summary>
        /// Set working directory
        /// </summary>
        /// <param name="workDir"></param>
        /// <returns></returns>
        public CommandLine SetWorkingDirectory(string workDir)
        {
            Expect.DirectoryExist(workDir);

            _workDir = workDir;
            return this;
        }

        /// <summary>
        /// Add argument
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public CommandLine AddArg(string arg)
        {
            Expect.IsNotBlank(arg, nameof(arg));

            _args.Add(arg);
            return this;
        }

        /// <summary>
        /// Helper method to execute database management command and return standard output.
        /// </summary>
        /// <returns></returns>
        public string Exec()
        {
            var psi = new ProcessStartInfo()
            {
                FileName = _cmd,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                Arguments = string.Join(" ", _args)
            };
            if (_workDir.IsNotBlank())
                psi.WorkingDirectory = _workDir;

            using (var process = Process.Start(psi))
            {
                process.WaitForExit();
                return process.StandardOutput.ReadToEnd();
            }
        }
    }
}
