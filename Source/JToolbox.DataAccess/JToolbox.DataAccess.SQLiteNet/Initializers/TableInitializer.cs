using JToolbox.DataAccess.SQLiteNet.Entities;
using SQLite;
using System.Linq;

namespace JToolbox.DataAccess.SQLiteNet.Initializers
{
    public class TableInitializer
    {
        public virtual void Run(SQLiteConnection db)
        {
            var entityTypes = typeof(BaseEntity).Assembly
               .GetTypes()
               .Where(t => typeof(BaseEntity).IsAssignableFrom(t) && !t.IsAbstract);
            foreach (var entityType in entityTypes)
            {
                db.CreateTable(entityType);
            }
        }
    }
}