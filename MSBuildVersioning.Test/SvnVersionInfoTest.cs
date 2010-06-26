using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MSBuildVersioning.Test
{
    [TestFixture]
    public class SvnVersionInfoTest
    {
        private SvnVersionInfo CreateSvnVersionInfo()
        {
            return new SvnVersionInfo() { Path = @"C:\Temp\SvnTest\More" };
        }

        [Test]
        public void GetRevisionNumberTest()
        {
            int revisionNumber = CreateSvnVersionInfo().GetRevisionNumber();
            Assert.AreEqual(2, revisionNumber);
        }

        [Test]
        public void IsMixedRevisionsTest()
        {
            CreateSvnVersionInfo().IsMixedRevisions();
        }

        [Test]
        public void IsWorkingCopyDirtyTest()
        {
            CreateSvnVersionInfo().IsWorkingCopyDirty();
        }

        [Test]
        public void GetBranchTest()
        {
            CreateSvnVersionInfo().GetBranch();
        }

        [Test]
        public void GetTagTest()
        {
            CreateSvnVersionInfo().GetTag();
        }
    }
}
