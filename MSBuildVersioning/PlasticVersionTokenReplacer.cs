namespace MSBuildVersioning
{
    public class PlasticVersionTokenReplacer : VersionTokenReplacer
    {
        public PlasticVersionTokenReplacer(PlasticInfoProvider infoProvider)
        {
            AddToken("REVNUM", () => infoProvider.GetRevisionNumber().ToString());
            AddToken("REVNUM_MOD", x => (infoProvider.GetRevisionNumber() % x).ToString());
            AddToken("REVNUM_DIV", x => (infoProvider.GetRevisionNumber() / x).ToString());
            AddToken("REVID", () => infoProvider.GetRevisionId());
            AddToken("DIRTY", () => infoProvider.IsWorkingCopyDirty() ? "1" : "0");
            AddToken("BRANCH", () => infoProvider.GetBranch());
            //AddToken("TAGS", () => infoProvider.GetTags());
        }
    }
}
