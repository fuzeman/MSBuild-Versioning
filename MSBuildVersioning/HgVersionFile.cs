using System;

namespace MSBuildVersioning
{
    public class HgVersionFile : AbstractVersionFile
    {
        protected override string ReplaceTokens(string content)
        {
            HgVersionInfo info = new HgVersionInfo();

            if (content.Contains("$REVNUM$"))
            {
                content = content.Replace("$REVNUM$", info.GetRevisionNumber().ToString());
            }
            if (content.Contains("$REVID$"))
            {
                content = content.Replace("$REVID$", info.GetRevisionId());
            }
            if (content.Contains("$DIRTY$"))
            {
                content = content.Replace("$DIRTY$", info.IsWorkingCopyDirty() ? "1" : "0");
            }
            if (content.Contains("$BRANCH$"))
            {
                content = content.Replace("$BRANCH$", info.GetBranch());
            }
            if (content.Contains("$TAGS$"))
            {
                content = content.Replace("$TAGS$", info.GetTags());
            }
            return content;
        }
    }
}
