using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MSBuildVersioning.Test
{
    [TestFixture]
    public class HgVersionInfoTest
    {
        [Test]
        public void GetRevisionNumberTest()
        {
            int revisionNumber = new HgVersionInfo().GetRevisionNumber();
            Assert.Greater(revisionNumber, 0);
        }
    }
}
