using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;

namespace MSBuildVersioning
{
    public abstract class AbstractVersionInfo
    {
        protected AbstractVersionInfo()
        {
            Path = "";
        }

        public string Path { get; set; }

        public abstract string SourceControlName { get; }

        protected virtual IList<string> ExecuteCommand(string fileName, string arguments)
        {
            IList<string> output = new List<string>();
            ExecuteCommand(fileName, arguments, outputLine => output.Add(outputLine));
            return output;
        }

        protected virtual void ExecuteCommand(string fileName, string arguments,
            Action<string> outputHandler)
        {
            StringBuilder error = new StringBuilder();

            using (Process process = new Process())
            {
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.WorkingDirectory = Path;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.OutputDataReceived += (o, e) =>
                    {
                        if (e.Data != null)
                            outputHandler(e.Data);
                    };

                process.ErrorDataReceived += (o, e) =>
                    {
                        if (e.Data != null)
                            error.AppendLine(e.Data);
                    };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();

                if (process.ExitCode != 0 || error.Length > 0)
                {
                    throw new BuildErrorException(String.Format(
                        "{0} command \"{1} {2}\" exited with code {3}.\n{4}",
                        SourceControlName, fileName, arguments, process.ExitCode, error));
                }
            }
        }
    }
}
