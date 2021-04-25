using System.Data;

namespace JToolbox.Misc.DbAccess
{
    public interface IDbFactory
    {
        IDbConnection CreateConnection();

        IDbDataAdapter GetDataAdapter();
    }
}