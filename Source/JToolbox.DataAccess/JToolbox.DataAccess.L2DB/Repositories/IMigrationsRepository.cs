using LinqToDB.Data;

namespace JToolbox.DataAccess.L2DB.Repositories
{
    public interface IMigrationsRepository
    {
        int GetDbVersion(DataConnection db);
    }
}