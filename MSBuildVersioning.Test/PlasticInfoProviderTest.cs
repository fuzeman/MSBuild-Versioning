using NUnit.Framework;

namespace MSBuildVersioning.Test
{
    [TestFixture]
    public class PlasticInfoProviderTest
    {
        public PlasticInfoProvider Plastic1
        {
            get { return new PlasticInfoProvider {Path = @"C:\Temp\TestRepositories\Plastic1"}; }
        }

        public PlasticInfoProvider Plastic2
        {
            get { return new PlasticInfoProvider { Path = @"C:\Temp\TestRepositories\Plastic2" }; }
        }

        [Test]
        public void GetRevisionNumberTest()
        {
            Assert.AreEqual(3, Plastic1.GetRevisionNumber());
            Assert.AreEqual(6, Plastic2.GetRevisionNumber());
        }

        [Test]
        public void GetRevisionIdTest()
        {
            Assert.AreEqual("3", Plastic1.GetRevisionId());
            Assert.AreEqual("6", Plastic2.GetRevisionId());
        }

        [Test]
        public void IsWorkingCopyDirtyTest()
        {
            Assert.IsTrue(Plastic1.IsWorkingCopyDirty());
            Assert.IsFalse(Plastic2.IsWorkingCopyDirty());
        }

        [Test]
        public void GetBranchTest()
        {
            Assert.AreEqual("main", Plastic1.GetBranch());
            Assert.AreEqual("main/test", Plastic2.GetBranch());
        }
    }
}
