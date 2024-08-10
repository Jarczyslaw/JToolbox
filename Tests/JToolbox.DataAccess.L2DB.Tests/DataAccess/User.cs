using JToolbox.DataAccess.L2DB.MySql.Entities;
using LinqToDB.Mapping;

namespace JToolbox.DataAccess.L2DB.Tests.DataAccess
{
    [Table("Users")]
    public class User : BaseExtendedEntity
    {
        [Column]
        public int Age { get; set; }

        [Column]
        public bool IsActive { get; set; }

        [Column]
        public string Name { get; set; }
    }
}