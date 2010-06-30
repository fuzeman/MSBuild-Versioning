using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;

namespace MSBuildVersioning.Test
{
    [TestFixture]
    public class HgVersionFileTest
    {
        [Test]
        public void ReplaceTokensTest()
        {
            var infoMock = new Mock<HgVersionInfo>();
            infoMock.Setup(x => x.GetRevisionNumber()).Returns(1437);

            string content = "Revision 1.0.$REVNUM_DIV(100)$.$REVNUM_MOD(100)$";
            content = new HgVersionFile().ReplaceTokens(content, infoMock.Object);

            Assert.AreEqual("Revision 1.0.14.37", content);
        }
    }
}
