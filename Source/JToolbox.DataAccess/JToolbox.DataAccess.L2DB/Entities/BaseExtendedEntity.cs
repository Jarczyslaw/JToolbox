using LinqToDB.Mapping;
using System;

namespace JToolbox.DataAccess.L2DB.Entities
{
    public abstract class BaseExtendedEntity : BaseEntity
    {
        [Column, NotNull]
        public DateTime CreateDate { get; set; }

        [Column, NotNull]
        public bool IsDeleted { get; set; }

        [Column, NotNull]
        public DateTime UpdateDate { get; set; }
    }
}