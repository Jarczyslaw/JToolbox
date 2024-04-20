using JToolbox.DataAccess.L2DB.Entities;
using LinqToDB;
using System;
using System.Threading.Tasks;

namespace JToolbox.DataAccess.L2DB
{
    public class DataAccessService : IDataAccessService
    {
        private readonly BaseInitializer _initializer;
        private readonly BaseLocker _locker;

        public DataAccessService(
            BaseInitializer initializer,
            BaseLocker dataBaseLocker)
        {
            _initializer = initializer;
            _locker = dataBaseLocker;
        }

        public bool AutoCommit { get; set; } = true;

        public string ConnectionString { get; set; }

        public void Execute(Action<BaseDbContext> action)
        {
            using (BaseDbContext db = CreateContext())
            {
                action(db);
            }
        }

        public T Execute<T>(Func<BaseDbContext, T> action)
        {
            using (BaseDbContext db = CreateContext())
            {
                return action(db);
            }
        }

        public void ExecuteTransaction(Action<BaseDbContext> action)
        {
            BaseDbContext db = null;
            try
            {
                db = CreateContext();
                db.BeginTransaction();
                action(db);

                if (AutoCommit) { db.CommitTransaction(); }
            }
            catch
            {
                db?.RollbackTransaction();
                throw;
            }
            finally
            {
                db?.Dispose();
            }
        }

        public T ExecuteTransaction<T>(Func<BaseDbContext, T> action)
        {
            T result;
            BaseDbContext db = null;
            try
            {
                db = CreateContext();
                db.BeginTransaction();
                result = action(db);

                if (AutoCommit) { db.CommitTransaction(); }
            }
            catch
            {
                db?.RollbackTransaction();
                throw;
            }
            finally
            {
                db?.Dispose();
            }
            return result;
        }

        public Task Init()
        {
            return Task.Run(() =>
            {
                try
                {
                    bool lockAcquired = _locker.AcquireLock(ConnectionString);
                    if (!lockAcquired)
                    {
                        throw new Exception("Database is locked by initialization. Please try again later");
                    }

                    ExecuteTransaction(x =>
                    {
                        x.CreateTable<MigrationEntity>(tableOptions: TableOptions.CreateIfNotExists);

                        _initializer.InitializeMigrations(x);
                    });
                }
                finally
                {
                    _locker.ReleaseLock();
                }
            });
        }

        private BaseDbContext CreateContext()
        {
            DataOptions options = _initializer.GetDataOptions
                .UseConnectionString(ConnectionString);

            return new BaseDbContext(options);
        }
    }
}