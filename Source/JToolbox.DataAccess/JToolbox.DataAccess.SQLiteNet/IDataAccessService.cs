using SQLite;
using System;
using System.Threading.Tasks;

namespace JToolbox.DataAccess.SQLiteNet
{
    public interface IDataAccessService
    {
        bool CacheConnection { get; set; }

        string DataSource { get; }

        TimeSpan Timeout { get; set; }

        Action<string> Tracer { set; }

        bool UseMigrationLockFile { get; set; }

        void Execute(Action<SQLiteConnection> action);

        T Execute<T>(Func<SQLiteConnection, T> action);

        void ExecuteTransaction(Action<SQLiteConnection> action);

        T ExecuteTransaction<T>(Func<SQLiteConnection, T> action);

        Task Init(string dataSource, string password);

        void Optimize();

        void Vacuum();
    }
}