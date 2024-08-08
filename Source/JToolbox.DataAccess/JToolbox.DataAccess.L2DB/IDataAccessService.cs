using System;
using System.Threading.Tasks;

namespace JToolbox.DataAccess.L2DB
{
    public interface IDataAccessService
    {
        bool AutoCommit { get; set; }

        string ConnectionString { get; set; }

        void Execute(Action<BaseDbContext> action);

        T Execute<T>(Func<BaseDbContext, T> action);

        void ExecuteTransaction(Action<BaseDbContext> action);

        T ExecuteTransaction<T>(Func<BaseDbContext, T> action);

        Task Init();
    }
}