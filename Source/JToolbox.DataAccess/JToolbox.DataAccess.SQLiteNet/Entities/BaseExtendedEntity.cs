using SQLite;
using System;

namespace JToolbox.DataAccess.SQLiteNet.Entities
{
    public abstract class BaseExtendedEntity : BaseEntity
    {
        [NotNull]
        public bool Locked { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        [NotNull]
        public bool Deleted { get; set; }
    }
}