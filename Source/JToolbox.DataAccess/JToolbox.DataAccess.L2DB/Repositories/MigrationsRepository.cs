using JToolbox.DataAccess.L2DB.Entities;
using LinqToDB;
using LinqToDB.Data;
using System.Linq;

namespace JToolbox.DataAccess.L2DB.Repositories
{
    public class MigrationsRepository : BaseRepository<MigrationEntity>, IMigrationsRepository
    {
        public int GetDbVersion(DataConnection db)
        {
            return db.GetTable<MigrationEntity>().Max(x => x.Version);
        }
    }
}