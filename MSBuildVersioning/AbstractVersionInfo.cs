using System;
using System.Diagnostics;
using System.Text;

namespace MSBuildVersioning
{
    public abstract class AbstractVersionInfo
    {
        public abstract string SourceControlName { get; }

        protected virtual string ExecuteCommand(string fileName, string arguments)
        {
            StringBuilder output = new StringBuilder();
            StringBuilder error = new StringBuilder();

            using (Process process = new Process())
            {
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.OutputDataReceived += (o, e) => output.AppendLine(e.Data);
                process.ErrorDataReceived += (o, e) => error.AppendLine(e.Data);
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();

                if (process.ExitCode != 0 || error.ToString().Trim().Length > 0)
                {
                    throw new BuildErrorException(String.Format(
                        "{0} command \"{1} {2}\" exited with code {3}.\n{4}",
                        SourceControlName, fileName, arguments, process.ExitCode, error));
                }
            }

            return output.ToString().Trim();
        }
    }
}
