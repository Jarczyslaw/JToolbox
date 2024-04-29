using LinqToDB;
using LinqToDB.Data;

namespace JToolbox.DataAccess.L2DB
{
    public class BaseDbContext : DataConnection
    {
        public BaseDbContext(DataOptions dataOptions) : base(dataOptions)
        {
        }
    }
}