using System.Data.Entity;

namespace JToolbox.DataAccess.EntityFrameworkAccess
{
    public abstract class BaseContext : DbContext
    {
        public BaseContext(string nameOrConnectionString)
           : base(nameOrConnectionString)
        {
            Configuration.ProxyCreationEnabled = false;
        }
    }
}