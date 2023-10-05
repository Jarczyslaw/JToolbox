using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JToolbox.DataAccess.SQLiteNet
{
    public class DataAccessService : IDataAccessService
    {
        private readonly BaseInitializer initializer;
        private SQLiteConnection connection;
        private TimeSpan timeSpan;
        private Action<string> tracer;

        public DataAccessService(BaseInitializer initializer)
        {
            this.initializer = initializer;
        }

        public bool CacheConnection { get; set; } = true;

        public string DataSource { get; private set; }

        public string Password { get; private set; }

        public TimeSpan Timeout
        {
            get => timeSpan;
            set
            {
                timeSpan = value;
                if (connection != null)
                {
                    connection.BusyTimeout = timeSpan;
                }
            }
        }

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

        public bool UseMigrationLockFile { get; set; }

        private SQLiteConnectionString ConnectionString => new SQLiteConnectionString(DataSource, false, key: Password);

        private string MigrationLockFile => Path.Combine(Path.GetDirectoryName(DataSource), "migration-lock");

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
            catch
            {
                db?.Rollback();
                throw;
            }
            finally
            {
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
            catch
            {
                db?.Rollback();
                throw;
            }
            finally
            {
                CloseConnection(false);
            }
        }

        public Task Init(string dataSource, string password)
        {
            if (DataSource != dataSource)
            {
                DataSource = dataSource;
                Password = password;
                return Task.Run(async () =>
                {
                    await CheckMigrationLock();

                    try
                    {
                        CreateMigrationLock();

                        var migrationApplied = false;
                        ExecuteTransaction(db =>
                        {
                            initializer.InitializeTables(db);
                            migrationApplied = initializer.InitializeMigrations(db);
                            initializer.InitializeData(db);
                        });

                        if (migrationApplied)
                        {
                            Vacuum();
                            Optimize();
                        }
                    }
                    finally
                    {
                        RemoveMigrationLock();
                    }
                });
            }
            return Task.CompletedTask;
        }

        public void Optimize() => Execute(db => db.Execute("PRAGMA OPTIMIZE"));

        public void Vacuum() => Execute(db => db.Execute("VACUUM"));

        private async Task CheckMigrationLock()
        {
            if (!UseMigrationLockFile) { return; }

            int attempts = 30;
            for (int i = 1; i <= attempts; i++)
            {
                if (!File.Exists(MigrationLockFile))
                {
                    return;
                }

                if (i != attempts)
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }
            throw new Exception("Database is locked by initialization. Please try again later");
        }

        private SQLiteConnection CreateConnection()
        {
            var connection = new SQLiteConnection(ConnectionString)
            {
                BusyTimeout = timeSpan
            };
            SetTracer(connection);
            return connection;
        }

        private void CreateMigrationLock()
        {
            if (UseMigrationLockFile)
            {
                using (File.Create(MigrationLockFile)) { }
            }
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

        private void RemoveMigrationLock()
        {
            if (UseMigrationLockFile && File.Exists(MigrationLockFile))
            {
                File.Delete(MigrationLockFile);
            }
        }

        private void SetTracer(SQLiteConnection connection)
        {
            connection.Trace = tracer != null;
            connection.Tracer = tracer;
        }
    }
}