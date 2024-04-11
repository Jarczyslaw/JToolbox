using JToolbox.Core.Abstraction;
using LinqToDB.Mapping;

namespace JToolbox.DataAccess.L2DB.Entities
{
    public abstract class BaseEntity : IKey
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        public string GetColumnName(string propertyName) => GetType().GetColumnName(propertyName);

        public string GetTableName() => GetType().GetTableName();
    }
}