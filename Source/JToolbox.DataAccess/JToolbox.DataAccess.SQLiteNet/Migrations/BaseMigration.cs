using SQLite;

namespace JToolbox.DataAccess.SQLiteNet.Migrations
{
    public abstract class BaseMigration
    {
        public abstract void Up(SQLiteConnection db);
    }
}