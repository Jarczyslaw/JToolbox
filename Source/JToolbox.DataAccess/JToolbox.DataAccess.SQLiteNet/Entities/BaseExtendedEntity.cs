using SQLite;
using System;

namespace JToolbox.DataAccess.SQLiteNet.Entities
{
    public abstract class BaseExtendedEntity : BaseEntity
    {
        public DateTime CreateDate { get; set; }

        [NotNull]
        public bool IsDeleted { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}