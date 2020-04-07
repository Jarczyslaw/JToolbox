using System.Data;

namespace JToolbox.DbAccess
{
    public interface IDbFactory
    {
        IDbConnection CreateConnection();

        IDbDataAdapter GetDataAdapter();
    }
}