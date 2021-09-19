using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Misc.Threading
{
    public abstract class ProducerConsumer<T>
    {
        private readonly BlockingCollection<T> items = new BlockingCollection<T>();
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        private Task task;

        protected ProducerConsumer()
        {
            Start();
        }

        public int PendingTasks => items.Count;

        public void Add(T item)
        {
            items.Add(item);
        }

        public async Task Cancel()
        {
            if (!tokenSource.IsCancellationRequested)
            {
                tokenSource.Cancel();
                await Task.WhenAll(task);
            }
        }

        public abstract Task ExceptionOccured(T item, Exception exc);

        public abstract Task HandleItem(T item);

        private void Start()
        {
            var token = tokenSource.Token;
            task = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    T item = default;
                    try
                    {
                        item = items.Take(token);
                        await HandleItem(item);
                    }
                    catch (OperationCanceledException)
                    {
                    }
                    catch (Exception exc)
                    {
                        await ExceptionOccured(item, exc);
                    }
                }
            }, token);
        }
    }
}