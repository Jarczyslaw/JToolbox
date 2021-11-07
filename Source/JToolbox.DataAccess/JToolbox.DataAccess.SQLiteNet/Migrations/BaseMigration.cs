using JToolbox.Core.Extensions;
using SQLite;

namespace JToolbox.DataAccess.SQLiteNet.Migrations
{
    public abstract class BaseMigration
    {
        public int? GetVersion()
        {
            var version = GetType().Name.ExtractBetween("Migration", "_");
            if (string.IsNullOrEmpty(version))
            {
                return null;
            }

            if (int.TryParse(version, out int ver))
            {
                return ver;
            }
            return null;
        }

        public abstract void Up(SQLiteConnection db);
    }
}