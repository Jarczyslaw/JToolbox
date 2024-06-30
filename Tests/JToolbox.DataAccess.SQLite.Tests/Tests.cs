using JToolbox.Core.Utilities;
using JToolbox.DataAccess.SQLite.Tests.DataAccess;
using JToolbox.DataAccess.SQLiteNet;

namespace JToolbox.DataAccess.SQLite.Tests
{
    [TestClass]
    public class Tests : BaseTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            InitializeDatabase();
        }

        [TestMethod]
        public void Test()
        {
            DataAccessService service = CreateDataAccessService();
            service.Init(DatabasePath, Password, true);

            int usersCount = 1000;
            List<User> users = [];
            for (int i = 0; i < usersCount; i++)
            {
                users.Add(new User
                {
                    Name = $"Name_{i}"
                });
            }

            ExecutionTime.RunAction(() => service.Execute(x => x.InsertAll(users)), "Inserting rows");

            int usersRowsCount = ExecutionTime.RunFunc(() => service.Execute(x => x.Table<User>().Count()), "Get rows count");

            Assert.AreEqual(usersCount, usersRowsCount);
        }
    }
}