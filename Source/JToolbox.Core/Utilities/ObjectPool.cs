using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Core.Utilities
{
    public abstract class ObjectPool<T>
        where T : class
    {
        private readonly Queue<T> _instances = new Queue<T>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        protected ObjectPool(int maxInstancesCount)
        {
            MaxInstancesCount = maxInstancesCount;
        }

        public int MaxInstancesCount { get; }

        public async Task ExecuteInPoolContext(Func<T, Task> action)
        {
            T instance = null;

            try
            {
                instance = await Get();
                await action(instance);
            }
            finally
            {
                await Return(instance);
            }
        }

        public async Task<T> Get()
        {
            T instance = null;

            await _semaphore.WaitAsync();

            try
            {
                if (_instances.Count > 0)
                {
                    instance = _instances.Dequeue();
                }
            }
            finally
            {
                _semaphore.Release();
            }

            instance = instance ?? await CreateNewInstance();

            if (instance == null) { throw new InvalidOperationException("Can not get or create instance from pool"); }

            await OnInstanceGet(instance);

            return instance;
        }

        public async Task Return(T instance)
        {
            if (instance == null) { return; }

            await _semaphore.WaitAsync();

            try
            {
                if (_instances.Count >= MaxInstancesCount)
                {
                    await OnPoolLimitExceeded();
                    return;
                }

                _instances.Enqueue(instance);

                await OnInstanceReturn(instance);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        protected abstract Task<T> CreateNewInstance();

        protected virtual Task OnInstanceGet(T instance) => Task.CompletedTask;

        protected virtual Task OnInstanceReturn(T instance) => Task.CompletedTask;

        protected virtual Task OnPoolLimitExceeded() => Task.CompletedTask;
    }
}