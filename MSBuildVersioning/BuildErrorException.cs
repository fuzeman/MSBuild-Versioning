using System;

namespace MSBuildVersioning
{
    public class BuildErrorException : Exception
    {
        public BuildErrorException(string message)
            : base(message) { }
    }
}
