using System.Data.Entity;

namespace JToolbox.DataAccess.EF
{
    public abstract class BaseContext : DbContext
    {
        protected BaseContext()
        {
        }

        protected BaseContext(string nameOrConnectionString)
           : base(nameOrConnectionString)
        {
            Configuration.ProxyCreationEnabled = false;
        }
    }
}