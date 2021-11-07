using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace JToolbox.DataAccess.EF
{
    public abstract class BaseConfiguration<T> : DbMigrationsConfiguration<T>
         where T : DbContext
    {
        public BaseConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}