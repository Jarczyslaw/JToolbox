using JToolbox.Core.TimeProvider;
using JToolbox.DataAccess.L2DB.Tests.DataAccess;
using LinqToDB;
using LinqToDB.Data;

namespace JToolbox.DataAccess.L2DB.Tests
{
    public class BaseTest
    {
        protected readonly List<User> _initialData = new()
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

        private const string _databaseName = "l2dbtests";
        private static string _connectionString;

        public static void InitializeDatabase()
        {
            _connectionString = GetConnectionString();

            Execute(x => x.CreateTable<User>(tableOptions: TableOptions.CheckExistence));
        }

        public void InitializeTable()
        {
            UsersRepository repository = new(new LocalTimeProvider());
            Execute(x => x.Execute($"TRUNCATE TABLE {typeof(User).GetTableName()}"));
            Execute(x => repository.CreateMany(x, _initialData, bulkCopyOptions: new BulkCopyOptions
            {
                KeepIdentity = true,
                BulkCopyType = BulkCopyType.MultipleRows
            }));
        }

        protected static void Execute(Action<DbContext> action)
        {
            DataOptions options = new DataOptions().UseMySqlConnector()
                .UseConnectionString(_connectionString);

            using var db = new DbContext(options);
            action(db);
        }

        protected static T Execute<T>(Func<DbContext, T> func)
        {
            DataOptions options = new DataOptions().UseMySqlConnector()
                .UseConnectionString(_connectionString);

            using var db = new DbContext(options);
            return func(db);
        }

        private static string GetConnectionString()
        {
            string password = File.ReadAllText($"C:\\{_databaseName}\\rootpw");
            return $"Server=127.0.0.1;Database={_databaseName};Uid=root;Pwd={password};";
        }
    }
}