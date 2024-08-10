using JToolbox.DataAccess.L2DB.Abstraction;
using LinqToDB.Mapping;
using System;

namespace JToolbox.DataAccess.L2DB.MySql.Entities
{
    [Table("_Migrations")]
    public class MigrationEntity : BaseEntity, IMigrationEntity
    {
        [Column(Precision = 3), NotNull]
        public DateTime ExecutionDate { get; set; }

        [Column, NotNull]
        public string Name { get; set; }

        [Column, NotNull]
        public int Version { get; set; }
    }
}