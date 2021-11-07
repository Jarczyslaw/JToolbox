using JToolbox.DataAccess.SQLiteNet.Entities;
using SQLite;
using System.Linq;

namespace JToolbox.DataAccess.SQLiteNet.Repositories
{
    public class MigrationsRepository : BaseRepository<MigrationEntity>, IMigrationsRepository
    {
        public int GetDbVersion(SQLiteConnection db)
        {
            return GetAll(db).Max(x => x.Version);
        }
    }
}