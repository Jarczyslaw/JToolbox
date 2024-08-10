using LinqToDB;

namespace JToolbox.DataAccess.L2DB
{
    public interface IBaseInitializer
    {
        DataOptions GetDataOptions { get; }

        void CreateMigrationsTableIfNotExists(BaseDbContext db);

        int GetDbVersion(BaseDbContext db);

        void InitializeMigrations(BaseDbContext db);
    }
}