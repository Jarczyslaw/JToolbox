using System;

namespace JToolbox.DataAccess.L2DB.Abstraction
{
    public interface IMigrationEntity : IBaseEntity
    {
        DateTime ExecutionDate { get; set; }

        string Name { get; set; }

        int Version { get; set; }
    }
}