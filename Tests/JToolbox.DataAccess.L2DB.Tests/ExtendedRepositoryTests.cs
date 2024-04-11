namespace JToolbox.DataAccess.L2DB.Tests
{
    [TestClass]
    public class ExtendedRepositoryTests : BaseTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            InitializeDatabase();
        }

        [TestMethod]
        public void Test() => Assert.AreEqual(1, 1);

        [TestInitialize]
        public void TestInitialize()
        {
            InitializeTable();
        }
    }
}