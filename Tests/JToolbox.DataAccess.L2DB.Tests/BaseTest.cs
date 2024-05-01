using JToolbox.Core.TimeProvider;
using JToolbox.DataAccess.L2DB.MySql;
using JToolbox.DataAccess.L2DB.Tests.DataAccess;
using LinqToDB;
using LinqToDB.Data;

namespace JToolbox.DataAccess.L2DB.Tests
{
    public class BaseTest
    {
        protected readonly List<Order> _initialOrders = new()
        {
            new Order
            {
                Name = "Order1",
                OrderType = OrderType.Advanced,
                IsActive = true,
                Items =
                [
                    new OrderItem
                    {
                        Name = "Item1",
                    },
                    new OrderItem
                    {
                        Name = "Item2",
                    }
                ]
            }
        };

        protected readonly List<User> _initialUsers = new()
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

            Execute(x => x.CreateTable<Order>(tableOptions: TableOptions.CheckExistence));
            Execute(x => x.CreateTable<OrderItem>(tableOptions: TableOptions.CheckExistence));

            Execute(x => x.CreateForeignKeyIfNotExists<OrderItem, Order>(
                nameof(OrderItem.OrderId),
                nameof(Order.Id)));
        }

        public void InitializeTables()
        {
            UsersRepository usersRepository = new(new LocalTimeProvider());
            Execute(x => x.Truncate<User>());
            Execute(x => usersRepository.CreateMany(x, _initialUsers, bulkCopyOptions: new BulkCopyOptions
            {
                KeepIdentity = true,
                BulkCopyType = BulkCopyType.MultipleRows
            }));

            OrdersRepository ordersRepository = new(new LocalTimeProvider());
            OrderItemsRepository orderItemsRepository = new();

            Execute(x =>
            {
                x.SetForeignKeyChecks(false);

                x.Truncate<OrderItem>();
                x.Truncate<Order>();

                x.SetForeignKeyChecks(true);

                _initialOrders.ForEach(y => ordersRepository.Create(x, y));
                _initialOrders.ForEach(x => x.Items.ForEach(y => y.OrderId = x.Id));

                orderItemsRepository.CreateMany(x, _initialOrders.SelectMany(y => y.Items).ToList());
            });
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