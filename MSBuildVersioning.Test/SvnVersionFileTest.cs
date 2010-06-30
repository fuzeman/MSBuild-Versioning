using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;

namespace MSBuildVersioning.Test
{
    [TestFixture]
    public class SvnVersionFileTest
    {
        [Test]
        public void ReplaceTokensTest()
        {
            var infoMock = new Mock<SvnVersionInfo>();
            infoMock.Setup(x => x.GetRevisionNumber()).Returns(1437);
            infoMock.Setup(x => x.GetRepositorySubDirectory("branches")).Returns("1.0-stable");

            string content =
                "Revision 1.0.$REVNUM_DIV(100)$.$REVNUM_MOD(100)$ of $SUBDIR(\"branches\")$";

            content = new SvnVersionFile().ReplaceTokens(content, infoMock.Object);

            Assert.AreEqual("Revision 1.0.14.37 of 1.0-stable", content);
        }
    }
}
