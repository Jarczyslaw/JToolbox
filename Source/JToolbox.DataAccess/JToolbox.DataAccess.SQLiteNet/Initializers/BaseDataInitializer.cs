using SQLite;

namespace JToolbox.DataAccess.SQLiteNet.Initializers
{
    public abstract class BaseDataInitializer
    {
        public virtual void Run(SQLiteConnection db)
        {
        }
    }
}