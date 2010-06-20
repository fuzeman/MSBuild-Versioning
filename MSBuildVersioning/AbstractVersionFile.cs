using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuildVersioning
{
    public abstract class AbstractVersionFile : Task
    {
        [Required]
        public string TemplateFile { get; set; }

        [Required]
        public string DestinationFile { get; set; }

        public override bool Execute()
        {
            try
            {
                // Read content of the template file
                string content = File.ReadAllText(TemplateFile);

                // Replace tokens in the template file content with version info
                content = ReplaceTokens(content);

                // Write the destination file, only if it needs to be updated
                if (!File.Exists(DestinationFile) || File.ReadAllText(DestinationFile) != content)
                {
                    File.WriteAllText(DestinationFile, content);
                }

                return true;
            }
            catch (BuildErrorException e)
            {
                Log.LogError(e.Message);
                return false;
            }
        }

        protected abstract string ReplaceTokens(string content);
    }
}
