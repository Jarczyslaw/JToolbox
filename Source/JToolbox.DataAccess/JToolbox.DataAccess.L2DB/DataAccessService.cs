using LinqToDB;
using System;
using System.Threading.Tasks;

namespace JToolbox.DataAccess.L2DB
{
    public class DataAccessService : IDataAccessService
    {
        private readonly IBaseInitializer _initializer;
        private readonly BaseLocker _locker;

        public DataAccessService(
            IBaseInitializer initializer,
            BaseLocker dataBaseLocker)
        {
            _initializer = initializer;
            _locker = dataBaseLocker;
        }

        public bool AutoCommit { get; set; } = true;

        public string ConnectionString { get; set; }

        public int GetDbVersion()
        {
            return RunFunction(x => _initializer.GetDbVersion(x));
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

                    RunActionTransaction(x =>
                    {
                        _initializer.CreateMigrationsTableIfNotExists(x);
                        _initializer.InitializeMigrations(x);
                    });
                }
                finally
                {
                    _locker.ReleaseLock();
                }
            });
        }

        public void RunAction(Action<BaseDbContext> action)
        {
            using (BaseDbContext db = CreateContext())
            {
                action(db);
            }
        }

        public void RunActionTransaction(Action<BaseDbContext> action)
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

        public T RunFunction<T>(Func<BaseDbContext, T> action)
        {
            using (BaseDbContext db = CreateContext())
            {
                return action(db);
            }
        }

        public T RunFunctionTransaction<T>(Func<BaseDbContext, T> action)
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

        private BaseDbContext CreateContext()
        {
            DataOptions options = _initializer.GetDataOptions
                .UseConnectionString(ConnectionString);

            return new BaseDbContext(options);
        }
    }
}