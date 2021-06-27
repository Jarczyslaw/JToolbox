using SQLite;
using System;

namespace JToolbox.DataAccess.SQLiteNet.Entities
{
    [Table("Migrations")]
    public class MigrationEntity : BaseEntity
    {
        public string Name { get; set; }
        public DateTime ExecutionDate { get; set; }
    }
}