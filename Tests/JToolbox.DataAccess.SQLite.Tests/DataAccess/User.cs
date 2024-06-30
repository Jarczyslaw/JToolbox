using JToolbox.DataAccess.SQLiteNet.Entities;
using SQLite;

namespace JToolbox.DataAccess.SQLite.Tests.DataAccess
{
    [Table("Users")]
    internal class User : BaseEntity
    {
        public string Name { get; set; }
    }
}