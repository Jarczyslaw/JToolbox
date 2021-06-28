using JToolbox.DataAccess.SQLiteNet.Initializers;
using SQLite;
using System;
using System.Threading.Tasks;

namespace JToolbox.DataAccess.SQLiteNet
{
    public class DataAccessService : IDataAccessService
    {
        private SQLiteConnection connection;

        private readonly BaseMigrationInitializer migrationInitializer;
        private readonly BaseTableInitializer tableInitializer;
        private readonly BaseDataInitializer dataInitializer;

        public DataAccessService(BaseMigrationInitializer migrationInitializer, BaseTableInitializer tableInitializer, BaseDataInitializer dataInitializer)
        {
            this.migrationInitializer = migrationInitializer;
            this.tableInitializer = tableInitializer;
            this.dataInitializer = dataInitializer;
        }

        private SQLiteConnectionString ConnectionString => new SQLiteConnectionString(DataSource, false, key: Password);

        public string DataSource { get; private set; }

        public string Password { get; private set; }

        public bool CacheConnection { get; set; } = true;

        private SQLiteConnection OpenConnection()
        {
            if (CacheConnection)
            {
                if (connection != null && connection.DatabasePath != DataSource)
                {
                    CloseConnection(true);
                }

                if (connection == null)
                {
                    connection = new SQLiteConnection(ConnectionString);
                }
            }
            else
            {
                connection?.Close();
                connection = new SQLiteConnection(ConnectionString);
            }
            return connection;
        }

        public void CloseConnection(bool forceClose)
        {
            if (!CacheConnection || forceClose)
            {
                connection?.Close();
                connection = null;
            }
        }

        public Task Init(string dataSource, string password)
        {
            if (DataSource != dataSource)
            {
                DataSource = dataSource;
                Password = password;
                return Task.Run(() =>
                {
                    ExecuteTransaction(db =>
                    {
                        tableInitializer.Run(db);
                        migrationInitializer.Run(db);
                        dataInitializer.Run(db);
                        return true;
                    });
                });
            }
            return Task.CompletedTask;
        }

        public void Execute(Action<SQLiteConnection> action)
        {
            try
            {
                action(OpenConnection());
            }
            finally
            {
                CloseConnection(false);
            }
        }

        public T Execute<T>(Func<SQLiteConnection, T> action)
        {
            try
            {
                return action(OpenConnection());
            }
            finally
            {
                CloseConnection(false);
            }
        }

        public void ExecuteTransaction(Action<SQLiteConnection> action)
        {
            SQLiteConnection db = null;
            try
            {
                db = OpenConnection();
                db.BeginTransaction();
                action(db);
                db.Commit();
            }
            finally
            {
                db.Rollback();
                CloseConnection(false);
            }
        }

        public T ExecuteTransaction<T>(Func<SQLiteConnection, T> action)
        {
            SQLiteConnection db = null;
            try
            {
                db = OpenConnection();
                db.BeginTransaction();
                var result = action(db);
                db.Commit();
                return result;
            }
            finally
            {
                db.Rollback();
                CloseConnection(false);
            }
        }
    }
}