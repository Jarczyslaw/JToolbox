using JToolbox.DataAccess.L2DB.Abstraction;
using LinqToDB.Mapping;

namespace JToolbox.DataAccess.L2DB.SQLite.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        public string GetColumnName(string propertyName) => GetType().GetColumnName(propertyName);

        public string GetTableName() => GetType().GetTableName();
    }
}