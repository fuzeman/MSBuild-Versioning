namespace MSBuildVersioning
{
    public class PlasticVersionFile : VersionFile
    {
        public PlasticVersionFile()
            : base(new PlasticVersionTokenReplacer(new PlasticInfoProvider()))
        {
        }
    }
}
