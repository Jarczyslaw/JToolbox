using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Threading
{
    public abstract class ProducerConsumer<T>
    {
        private readonly BlockingCollection<T> items = new BlockingCollection<T>();
        private Task task;
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();

        protected ProducerConsumer()
        {
            Start();
        }

        public int PendingTasks => items.Count;

        public abstract Task HandleItem(T item);

        public abstract Task ExceptionOccured(T item, Exception exc);

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
    }
}