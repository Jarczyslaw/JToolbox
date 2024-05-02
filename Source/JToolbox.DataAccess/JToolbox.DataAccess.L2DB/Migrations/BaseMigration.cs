using JToolbox.Core.Extensions;
using LinqToDB.Data;

namespace JToolbox.DataAccess.L2DB.Migrations
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

        public virtual void InitializeData(DataConnection db)
        { }

        public abstract void Up(DataConnection db, bool newDatabase);
    }
}