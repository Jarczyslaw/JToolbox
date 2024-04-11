using JToolbox.DataAccess.L2DB.Entities;
using LinqToDB.Mapping;

namespace JToolbox.DataAccess.L2DB.Tests.DataAccess
{
    [Table("Users")]
    public class User : BaseExtendedEntity
    {
        [Column, NotNull]
        public int Age { get; set; }

        [Column, NotNull]
        public bool IsActive { get; set; }

        [Column, NotNull]
        public string Name { get; set; }
    }
}