using JToolbox.Core.Abstraction;
using SQLite;

namespace JToolbox.DataAccess.SQLiteNet.Entities
{
    public abstract class BaseEntity : IKey
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}