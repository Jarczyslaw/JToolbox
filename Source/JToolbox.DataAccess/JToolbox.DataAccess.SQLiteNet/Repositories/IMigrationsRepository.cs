using SQLite;

namespace JToolbox.DataAccess.SQLiteNet.Repositories
{
    public interface IMigrationsRepository
    {
        int GetDbVersion(SQLiteConnection db);
    }
}