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
            Assert.IsTrue(revisionNumber > 0);
        }

        [Test]
        public void GetRevisionIdTest()
        {
            string revisionId = new HgVersionInfo().GetRevisionId();
            Assert.AreEqual(12, revisionId.Length);
        }

        [Test]
        public void IsWorkingCopyDirtyTest()
        {
            new HgVersionInfo().IsWorkingCopyDirty();
        }

        [Test]
        public void GetBranchTest()
        {
            string branch = new HgVersionInfo().GetBranch();
            Assert.IsTrue(branch.Length > 0);
        }

        [Test]
        public void GetTagsTest()
        {
            string tags = new HgVersionInfo().GetTags();
            Assert.IsNotNull(tags);
        }
    }
}
