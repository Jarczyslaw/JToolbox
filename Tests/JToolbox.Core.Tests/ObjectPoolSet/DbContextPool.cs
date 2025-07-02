using JToolbox.Core.Utilities;
using System.Threading.Tasks;

namespace JToolbox.Core.Tests.ObjectPoolSet
{
    public class DbContextPool : ObjectPool<DbContext>
    {
        private int _currentId = 1;

        public DbContextPool()
            : base(3)
        {
        }

        public int PoolLimitExceededTimes { get; private set; }

        protected override Task<DbContext> CreateNewInstance()
        {
            DbContext result = new DbContext(_currentId);
            _currentId++;

            return Task.FromResult(result);
        }

        protected override Task OnInstanceGet(DbContext instance)
        {
            instance.IsInUse = true;
            return Task.CompletedTask;
        }

        protected override Task OnInstanceReturn(DbContext instance)
        {
            instance.IsInUse = false;
            return Task.CompletedTask;
        }

        protected override Task OnPoolLimitExceeded()
        {
            PoolLimitExceededTimes++;
            return Task.CompletedTask;
        }
    }
}