using JToolbox.Core.TimeProvider;
using JToolbox.DataAccess.L2DB.SQLite.Entities;

namespace JToolbox.DataAccess.L2DB.SQLite
{
    public abstract class BaseSQLiteInitializer : BaseInitializer<MigrationEntity>
    {
        protected BaseSQLiteInitializer(ITimeProvider timeProvider)
            : base(timeProvider)
        {
        }
    }
}