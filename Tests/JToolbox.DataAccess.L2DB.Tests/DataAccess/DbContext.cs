using LinqToDB;

namespace JToolbox.DataAccess.L2DB.Tests.DataAccess
{
    public class DbContext : BaseDbContext
    {
        public DbContext(DataOptions dataOptions)
            : base(dataOptions)
        {
        }
    }
}