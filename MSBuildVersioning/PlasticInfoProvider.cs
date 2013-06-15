using System;

namespace MSBuildVersioning
{
    public class PlasticInfoProvider : SourceControlInfoProvider
    {
        private int? _revisionNumber;
        private string _revisionId;
        private bool? _isWorkingCopyDirty;
        private string _branch;
        private string _tags;

        public override string SourceControlName
        {
            get { return "Plastic SCM"; }
        }

        public virtual int GetRevisionNumber()
        {
            if (_revisionNumber == null)
            {
                GetStatus();
            }
            return (int) _revisionNumber;
        }

        public virtual string GetRevisionId()
        {
            if (_revisionId == null)
            {
                GetStatus();
            }
            return _revisionId;
        }

        private void GetStatus()
        {
            var status = ExecuteCommand("cm", "workspacestatus")[0];

            var start = status.IndexOf(":") + 1;
            _revisionId = status.Substring(start, status.IndexOf("@") - start);
            _revisionNumber = Convert.ToInt32(_revisionId);
        }

        public virtual bool IsWorkingCopyDirty()
        {
            if (_isWorkingCopyDirty == null)
            {
                ExecuteCommand("cm", "findchanged", s =>
                {
                    _isWorkingCopyDirty = true;
                }, null);

                if(_isWorkingCopyDirty == null)
                    _isWorkingCopyDirty = false;
            }

            return (bool) _isWorkingCopyDirty;
        }

        public virtual string GetBranch()
        {
            if (_branch == null)
            {
                _branch = ExecuteCommand("cm", "workspaceinfo")[0];
                var start = _branch.IndexOf("/") + 1;
                _branch = _branch.Substring(start, _branch.IndexOf("@") - start);
            }

            return _branch;
        }

        public virtual string GetTags()
        {
            throw new NotImplementedException();
        }
    }
}
