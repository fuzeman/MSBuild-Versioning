using System;
using System.Text.RegularExpressions;

namespace MSBuildVersioning
{
    public class SvnVersionFile : AbstractVersionFile
    {
        protected override string ReplaceTokens(string content)
        {
            return ReplaceTokens(content, new SvnVersionInfo());
        }

        public virtual string ReplaceTokens(string content, SvnVersionInfo info)
        {
            if (content.Contains("$REVNUM$"))
            {
                content = content.Replace("$REVNUM$", info.GetRevisionNumber().ToString());
            }

            MatchCollection revnumModMatches = Regex.Matches(content, @"\$REVNUM_MOD\((\d+)\)\$");
            foreach (Match match in revnumModMatches)
            {
                string token = match.Groups[0].Value;
                int mod = int.Parse(match.Groups[1].Value);
                content = content.Replace(token, (info.GetRevisionNumber() % mod).ToString());
            }

            MatchCollection revnumDivMatches = Regex.Matches(content, @"\$REVNUM_DIV\((\d+)\)\$");
            foreach (Match match in revnumDivMatches)
            {
                string token = match.Groups[0].Value;
                int div = int.Parse(match.Groups[1].Value);
                content = content.Replace(token, (info.GetRevisionNumber() / div).ToString());
            }

            if (content.Contains("$MIXED$"))
            {
                content = content.Replace("$MIXED$", info.IsMixedRevisions() ? "1" : "0");
            }
            if (content.Contains("$DIRTY$"))
            {
                content = content.Replace("$DIRTY$", info.IsWorkingCopyDirty() ? "1" : "0");
            }

            MatchCollection subDirMatches = Regex.Matches(content, @"\$SUBDIR\(""(.+?)""\)\$");
            foreach (Match match in subDirMatches)
            {
                string token = match.Groups[0].Value;
                string dir = match.Groups[1].Value;
                content = content.Replace(token, info.GetRepositorySubDirectory(dir));
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
