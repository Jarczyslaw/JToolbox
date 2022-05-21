using SQLite;
using System;
using System.Threading.Tasks;

namespace JToolbox.DataAccess.SQLiteNet
{
    public class DataAccessService : IDataAccessService
    {
        private readonly BaseInitializer initializer;
        private SQLiteConnection connection;
        private Action<string> tracer;

        public DataAccessService(BaseInitializer initializer)
        {
            this.initializer = initializer;
        }

        public bool CacheConnection { get; set; } = true;
        public string DataSource { get; private set; }
        public string Password { get; private set; }

        public Action<string> Tracer
        {
            set
            {
                tracer = value;
                if (connection != null)
                {
                    SetTracer(connection);
                }
            }
        }

        private SQLiteConnectionString ConnectionString => new SQLiteConnectionString(DataSource, false, key: Password);

        public void CloseConnection(bool forceClose)
        {
            if (!CacheConnection || forceClose)
            {
                connection?.Close();
                connection = null;
            }
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
                        initializer.InitializeTables(db);
                        initializer.InitializeMigrations(db);
                        initializer.InitializeData(db);
                        return true;
                    });
                });
            }
            return Task.CompletedTask;
        }

        private SQLiteConnection CreateConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            SetTracer(connection);
            return connection;
        }

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
                    connection = CreateConnection();
                }
            }
            else
            {
                connection?.Close();
                connection = CreateConnection();
            }
            return connection;
        }

        private void SetTracer(SQLiteConnection connection)
        {
            connection.Trace = tracer != null;
            connection.Tracer = tracer;
        }
    }
}