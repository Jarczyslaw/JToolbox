using JToolbox.Core.TimeProvider;
using JToolbox.DataAccess.L2DB.MySql;
using JToolbox.DataAccess.L2DB.Tests.DataAccess;
using LinqToDB;
using LinqToDB.Data;

namespace JToolbox.DataAccess.L2DB.Tests
{
    [TestClass]
    public class ExtendedRepositoryTests : BaseTest
    {
        private readonly UsersRepository _repository = new(new LocalTimeProvider());

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            InitializeDatabase();
        }

        [TestMethod]
        public void Any_ValidExpression_UserFound()
        {
            bool userExists = Execute(x => _repository.Any(x, y => y.Id == 1));

            Assert.IsTrue(userExists);
        }

        [TestMethod]
        public void Count_ValidExpression_ValidRowsCount()
        {
            int rowsCount = Execute(x => _repository.Count(x, y => y.Age >= 20));

            Assert.AreEqual(rowsCount, 2);
        }

        [TestMethod]
        public void Create_ValidUser_UserAdded()
        {
            var newUser = new User
            {
                Age = 40,
                Name = "User4",
                IsActive = true,
            };

            Execute(x => _repository.Create(x, newUser));

            User addedUser = Execute(x => _repository.GetFirstOrDefaultBy(x, y => y.Name == "User4"));

            Assert.AreEqual(addedUser.Age, newUser.Age);
            Assert.AreEqual(addedUser.IsActive, newUser.IsActive);
            Assert.IsTrue(addedUser.Id > 0);
        }

        [TestMethod]
        public void CreateMany_UsersWithIds_UsersAdded()
        {
            List<User> newUsers = new()
            {
                new User
                {
                    Age = 40,
                    Name = "User4",
                    IsActive = true,
                    Id = 100,
                },
                new User
                {
                    Age = 50,
                    Name = "User5",
                    IsActive = true,
                    Id = 101,
                },
            };

            Execute(x => _repository.CreateMany(x, newUsers, new BulkCopyOptions
            {
                KeepIdentity = true,
            }));

            List<int> ids = [100, 101];
            List<User> users = Execute(x => _repository.GetByIds(x, ids));

            Assert.AreEqual(newUsers.Count, users.Count);
            Assert.IsTrue(Enumerable.SequenceEqual(users.Select(x => x.Id), ids));
        }

        [TestMethod]
        public void Delete_ValidUser_UserDeleted()
        {
            Execute(x => _repository.Delete(x, new User { Id = 2 }));
            Execute(x => _repository.Delete(x, 3));
            Execute(x => _repository.DeleteBy(x, x => x.Id == 1));

            Assert.IsTrue(Execute(x => _repository.GetById(x, 2)) == null);
            Assert.IsFalse(Execute(x => _repository.EntityExists(x, x => x.Id == 3)));
            Assert.IsTrue(Execute(x => _repository.GetFirstOrDefaultBy(x, x => x.Id == 3)) == null);
        }

        [TestMethod]
        public void EntityExists_UserWithTheSameName_UserExists()
        {
            User newUser = new() { Name = "User3" };

            Assert.IsTrue(Execute(x => _repository.EntityExists(x, newUser, x => x.Name == "User3")));
        }

        [TestMethod]
        public void GetAndUpdate_UserWithNewAge_UserUpdated()
        {
            Execute(x => _repository.GetAndUpdate(x, x => x.Id == 1, x => x.Age = 100));
            User updatedUser = Execute(x => _repository.GetById(x, 1));

            Assert.AreEqual(100, updatedUser.Age);
        }

        [TestMethod]
        public void GetBy_UsersWithAgeHigherThan10_TwoUsersFetched()
        {
            List<User> users = Execute(x => _repository.GetBy(x, x => x.Age > 10));

            Assert.AreEqual(2, users.Count);
            Assert.IsTrue(users.All(x => x.Age > 10));
        }

        [TestMethod]
        public void GetByIds_TwoIdsProvided_TwoUsersFetched()
        {
            List<User> users = Execute(x => _repository.GetByIds(x, [1, 2]));

            Assert.AreEqual(2, users.Count);
        }

        [TestMethod]
        public void GetOrders_OrderWithItemsLoaded()
        {
            List<Order> orders = Execute(x => x.GetTable<Order>().LoadWith(y => y.Items).ThenLoad(x => x.Order).ToList());

            Assert.AreEqual(_initialOrders.Count, orders.Count);
            Assert.AreEqual(_initialOrders.Sum(x => x.Items.Count), orders.Sum(x => x.Items.Count));
            Assert.IsTrue(orders.SelectMany(x => x.Items).All(x => x.Order != null));
        }

        [TestMethod]
        public void IfForeignKeyExists_CheckForeignKeys()
        {
            string orderItemsTableName = typeof(OrderItem).GetTableName();
            string foreignKeyName = MySqlL2DbExtensions.GetForeignKeyName<OrderItem, Order>();

            bool keyExists = Execute(x => x.IfForeignKeyExists(orderItemsTableName, foreignKeyName));

            Assert.IsTrue(keyExists);
        }

        [TestMethod]
        public void Max_GetMaxIdFromEmptyTable_ZeroReturned()
        {
            int maxId = Execute(x => x.GetTable<OrderItem>().Max(x => x.Id));

            Execute(x => x.GetTable<OrderItem>().Delete());

            int newMaxId = Execute(x => x.GetTable<OrderItem>().Max(x => (int?)x.Id)) ?? 0;

            Assert.IsTrue(maxId > 0);
            Assert.AreEqual(0, newMaxId);
        }

        [TestMethod]
        public void SafeDelete_AllIdsProvided_AllUsersAreSafeDeleted()
        {
            Execute(x => _repository.SafeDelete(x, 1));
            Execute(x => _repository.SafeDelete(x, new List<int> { 2, 3 }));

            List<User> users = Execute(_repository.GetAll);

            Assert.AreEqual(3, users.Count);
            Assert.IsTrue(users.All(x => x.IsDeleted));
        }

        [TestInitialize]
        public void TestInitialize()
        {
            InitializeTables();
        }

        [TestMethod]
        public void UpdateMany_UpdateAge_TwoUsersUpdated()
        {
            List<User> usersToUpdate = new()
            {
                new User
                {
                    Id = 1,
                    Age = 100,
                    Name = "Updated1"
                },
                new User
                {
                    Id = 2,
                    Age = 200,
                    Name = "Updated2"
                }
            };

            int updatedRows = Execute(x => _repository.UpdateMany(x, usersToUpdate));

            Assert.AreEqual(2, updatedRows);
            Assert.AreEqual(Execute(x => _repository.GetById(x, 1)).Age, 100);
            Assert.AreEqual(Execute(x => _repository.GetById(x, 2)).Age, 200);
        }
    }
}