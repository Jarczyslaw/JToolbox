using Medallion.Threading.MySql;
using System;

namespace JToolbox.DataAccess.L2DB.MySql
{
    public class MySqlLocker : BaseLocker
    {
        private readonly string _name;
        private readonly TimeSpan? _timeout;
        private MySqlDistributedLock _lock;
        private MySqlDistributedLockHandle _lockHandle;

        public MySqlLocker(string name, TimeSpan? timeout)
        {
            _name = name;
            _timeout = timeout;
        }

        public override bool AcquireLock(string connectionString)
        {
            _lock = new MySqlDistributedLock(_name, connectionString);

            try
            {
                _lockHandle = _lock.Acquire(_timeout);
                return true;
            }
            catch (TimeoutException)
            {
                return false;
            }
        }

        public override void ReleaseLock()
        {
            _lockHandle?.Dispose();
        }
    }
}