using System;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Core.Utilities
{
    public class AsyncLock
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public async Task Lock(Func<Task> func)
        {
            await _semaphore.WaitAsync();
            try
            {
                await func();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}