using LinqToDB.Mapping;
using System;

namespace JToolbox.DataAccess.L2DB.Entities
{
    [Table("_Migrations")]
    public class MigrationEntity : BaseEntity
    {
        [Column(Precision = 3), NotNull]
        public DateTime ExecutionDate { get; set; }

        [Column, NotNull]
        public string Name { get; set; }

        [Column, NotNull]
        public int Version { get; set; }
    }
}