using System;
using System.Threading.Tasks;

namespace JToolbox.DataAccess.L2DB
{
    public interface IDataAccessService
    {
        bool AutoCommit { get; set; }

        string ConnectionString { get; set; }

        int GetDbVersion();

        Task Init();

        void RunAction(Action<BaseDbContext> action);

        void RunActionTransaction(Action<BaseDbContext> action);

        T RunFunction<T>(Func<BaseDbContext, T> action);

        T RunFunctionTransaction<T>(Func<BaseDbContext, T> action);
    }
}