using JToolbox.DataAccess.SQLiteNet.Entities;
using SQLite;
using System.Linq;
using System.Reflection;

namespace JToolbox.DataAccess.SQLiteNet.Initializers
{
    public abstract class BaseTableInitializer
    {
        protected virtual Assembly EntitiesAssembly { get; }

        public virtual void Run(SQLiteConnection db)
        {
            var entityTypes = EntitiesAssembly
               .GetTypes()
               .Where(t => typeof(BaseEntity).IsAssignableFrom(t) && !t.IsAbstract);
            foreach (var entityType in entityTypes)
            {
                db.CreateTable(entityType);
            }
        }
    }
}