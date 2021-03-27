using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Threading
{
    public abstract class ProcessingQueue<TItem, TResult>
    {
        public int TasksCount { get; set; } = 8;
        public bool StopOnFirstException { get; set; }

        private BlockingCollection<ProcessingQueueItem<TItem, TResult>> InitializeCollection(List<ProcessingQueueItem<TItem, TResult>> items)
        {
            var collection = new BlockingCollection<ProcessingQueueItem<TItem, TResult>>();
            foreach (var item in items)
            {
                item.Clear();
                collection.Add(item);
            }
            return collection;
        }

        public Task<List<ProcessingQueueItem<TItem, TResult>>> Run(List<TItem> items, CancellationToken cancellationToken = default)
        {
            return Run(items.ConvertAll(s => new ProcessingQueueItem<TItem, TResult>(s)), cancellationToken);
        }

        public async Task<List<ProcessingQueueItem<TItem, TResult>>> Run(List<ProcessingQueueItem<TItem, TResult>> items, CancellationToken cancellationToken = default)
        {
            var collection = InitializeCollection(items);
            var internalCancellationTokenSource = new CancellationTokenSource();
            var internalToken = internalCancellationTokenSource.Token;
            var tasks = new List<Task>();
            for (var i = 0; i < TasksCount; i++)
            {
                var task = Task.Run(async () =>
                {
                    while (!internalToken.IsCancellationRequested
                        && !cancellationToken.IsCancellationRequested)
                    {
                        collection.TryTake(out ProcessingQueueItem<TItem, TResult> item);
                        if (item != null)
                        {
                            item.Processed = true;
                            try
                            {
                                item.Result = await ProcessItem(item.Item);
                            }
                            catch (Exception exc)
                            {
                                item.Exception = exc;
                                if (StopOnFirstException)
                                {
                                    internalCancellationTokenSource.Cancel();
                                    return;
                                }
                            }
                            await ReportProgress(item);
                        }
                        else if (item == null && collection.Count == 0)
                        {
                            return;
                        }
                    }
                });
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
            return items;
        }

        public abstract Task<TResult> ProcessItem(TItem item);

        public abstract Task ReportProgress(ProcessingQueueItem<TItem, TResult> item);
    }
}