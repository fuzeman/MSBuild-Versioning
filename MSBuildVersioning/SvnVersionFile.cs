using System;
using System.Collections.Generic;
using System.Text;

namespace MSBuildVersioning
{
    public class SvnVersionFile : AbstractVersionFile
    {
        protected override string ReplaceTokens(string content)
        {
            SvnVersionInfo info = new SvnVersionInfo();

            if (content.Contains("$REVNUM$"))
            {
                content = content.Replace("$REVNUM$", info.GetRevisionNumber().ToString());
            }
            if (content.Contains("$MIXED$"))
            {
                content = content.Replace("$MIXED$", info.IsMixedRevisions() ? "1" : "0");
            }
            if (content.Contains("$DIRTY$"))
            {
                content = content.Replace("$DIRTY$", info.IsWorkingCopyDirty() ? "1" : "0");
            }
            if (content.Contains("$BRANCH$"))
            {
                content = content.Replace("$BRANCH$", info.GetBranch());
            }
            if (content.Contains("$TAG$"))
            {
                content = content.Replace("$TAG$", info.GetTag());
            }

            return content;
        }
    }
}
