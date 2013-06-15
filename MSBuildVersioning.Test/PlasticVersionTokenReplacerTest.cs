using Moq;
using NUnit.Framework;

namespace MSBuildVersioning.Test
{
    [TestFixture]
    public class PlasticVersionTokenReplacerTest
    {
        [Test]
        public void ReplaceTest()
        {
            var infoProviderMock = new Mock<PlasticInfoProvider>();
            infoProviderMock.Setup(x => x.GetRevisionNumber()).Returns(1437);

            var content = "Revision 1.0.$REVNUM_DIV(100)$.$REVNUM_MOD(100)$";
            content = new PlasticVersionTokenReplacer(infoProviderMock.Object).Replace(content);

            Assert.AreEqual("Revision 1.0.14.37", content);
        }
    }
}
