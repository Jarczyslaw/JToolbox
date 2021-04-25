using System;
using System.Data;

namespace JToolbox.Misc.DbAccess
{
    public class DbContext : IDisposable
    {
        public IDbFactory DbFactory { get; }

        public IDbConnection Connection { get; }
        public IDbCommand Command { get; }

        public DbContext(IDbFactory factory)
        {
            DbFactory = factory;
            Connection = DbFactory.CreateConnection();
            Command = Connection.CreateCommand();
            Connection.Open();
        }

        public virtual void Dispose()
        {
            Command.Dispose();
            Connection.Dispose();
        }
    }
}