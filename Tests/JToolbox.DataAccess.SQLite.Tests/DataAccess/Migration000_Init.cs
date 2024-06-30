using JToolbox.DataAccess.SQLiteNet.Migrations;
using SQLite;

namespace JToolbox.DataAccess.SQLite.Tests.DataAccess
{
    internal class Migration000_Init : BaseMigration
    {
        public override void Up(SQLiteConnection db, bool newDatabase)
        {
        }
    }
}