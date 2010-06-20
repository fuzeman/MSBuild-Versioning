using System;

namespace MSBuildVersioning
{
    public class HgVersionInfo : AbstractVersionInfo
    {
        private int? revisionNumber;
        private string revisionId;
        private bool? isWorkingCopyDirty;
        private string branch;
        private string tags;

        public override string SourceControlName
        {
            get { return "Mercurial"; }
        }

        public int GetRevisionNumber()
        {
            if (revisionNumber == null)
            {
                string revisionNumberStr = ExecuteCommand("hg.exe", "identify -n");

                if (revisionNumberStr.EndsWith("+"))
                {
                    isWorkingCopyDirty = true;
                    revisionNumber = int.Parse(
                        revisionNumberStr.Substring(0, revisionNumberStr.Length - 1));
                }
                else
                {
                    isWorkingCopyDirty = false;
                    revisionNumber = int.Parse(revisionNumberStr);
                }
            }
            return (int)revisionNumber;
        }

        public string GetRevisionId()
        {
            if (revisionId == null)
            {
                revisionId = ExecuteCommand("hg.exe", "identify -i");

                if (revisionId.EndsWith("+"))
                {
                    isWorkingCopyDirty = true;
                    revisionId = revisionId.Substring(0, revisionId.Length - 1);
                }
                else
                {
                    isWorkingCopyDirty = false;
                }
            }
            return revisionId;
        }

        public bool IsWorkingCopyDirty()
        {
            if (isWorkingCopyDirty == null)
            {
                GetRevisionNumber();
            }
            return (bool)isWorkingCopyDirty;
        }

        public string GetBranch()
        {
            if (branch == null)
            {
                branch = ExecuteCommand("hg.exe", "identify -b");
            }
            return branch;
        }

        public string GetTags()
        {
            if (tags == null)
            {
                tags = ExecuteCommand("hg.exe", "identify -t");
            }
            return tags;
        }
    }
}
