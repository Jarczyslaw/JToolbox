using JToolbox.Core.Abstraction;
using SQLite;

namespace JToolbox.DataAccess.SQLiteNet.Entities
{
    public abstract class BaseEntity : IKey
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string GetColumnName(string propertyName) => SqliteExtensions.GetColumnName(GetType(), propertyName);

        public string GetTableName() => SqliteExtensions.GetTableName(GetType());
    }
}