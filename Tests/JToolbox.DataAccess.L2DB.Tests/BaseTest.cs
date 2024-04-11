using JToolbox.Core.TimeProvider;
using JToolbox.DataAccess.L2DB.Tests.DataAccess;
using LinqToDB;

namespace JToolbox.DataAccess.L2DB.Tests
{
    public class BaseTest
    {
        private static string _connectionString;
        private static UsersRepository _repository;

        public static void InitializeDatabase()
        {
            _connectionString = GetConnectionString();
            _repository = new UsersRepository(new LocalTimeProvider());

            Execute(x =>
            {
                x.DropTable<User>(tableOptions: TableOptions.CheckExistence);
                x.CreateTable<User>();
            });
        }

        public void InitializeTable()
        {
            Execute(x =>
            {
                List<User> users = new()
                {
                    new User
                    {
                        Age = 10,
                        IsActive = true,
                        Name = "User1"
                    },
                    new User
                    {
                        Age = 20,
                        IsActive = true,
                        Name = "User2"
                    },
                    new User
                    {
                        Age = 30,
                        IsActive = true,
                        Name = "User3"
                    }
                };

                _repository.CreateMany(x, users);
            });
        }

        protected static void Execute(Action<DbContext> action)
        {
            DataOptions options = new DataOptions().UseMySqlConnector()
                .UseConnectionString(_connectionString);

            using (var db = new DbContext(options))
            {
                action(db);
            }
        }

        private static string GetConnectionString()
        {
            string password = File.ReadAllText("C:\\l2dbtests\\rootpw");
            return $"Server=127.0.0.1;Database=l2dbtests;Uid=root;Pwd={password};";
        }
    }
}