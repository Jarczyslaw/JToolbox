using System;

namespace JToolbox.DataAccess.L2DB.Abstraction
{
    public interface IBaseExtendedEntity : IBaseEntity
    {
        DateTime CreateDate { get; set; }

        bool IsDeleted { get; set; }

        DateTime UpdateDate { get; set; }
    }
}