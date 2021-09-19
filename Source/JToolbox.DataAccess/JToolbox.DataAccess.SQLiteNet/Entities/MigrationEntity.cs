using SQLite;
using System;

namespace JToolbox.DataAccess.SQLiteNet.Entities
{
    [Table("Migrations")]
    public class MigrationEntity : BaseEntity
    {
        public DateTime ExecutionDate { get; set; }
        public string Name { get; set; }
    }
}