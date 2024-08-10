using JToolbox.Core.TimeProvider;
using JToolbox.DataAccess.L2DB.MySql.Entities;

namespace JToolbox.DataAccess.L2DB.MySql
{
    public abstract class BaseMySqlInitializer : BaseInitializer<MigrationEntity>
    {
        protected BaseMySqlInitializer(ITimeProvider timeProvider)
            : base(timeProvider)
        {
        }
    }
}