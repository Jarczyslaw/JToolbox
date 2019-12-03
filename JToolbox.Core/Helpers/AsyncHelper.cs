using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Core.Helpers
{
    public static class AsyncHelper
    {
        private static readonly TaskFactory taskFactory = new
            TaskFactory(CancellationToken.None,
                        TaskCreationOptions.None,
                        TaskContinuationOptions.None,
                        TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
            => taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        public static void RunSync(Func<Task> func)
            => taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        public static Task ForEach<TItem>(IEnumerable<TItem> input, Func<TItem, Task> handler)
        {
            return ForEach(input, handler, CancellationToken.None);
        }

        public static async Task ForEach<TItem>(IEnumerable<TItem> input, Func<TItem, Task> handler, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();
            foreach (var item in input)
            {
                tasks.Add(Task.Run(async () => await handler(item), cancellationToken));
            }
            await Task.WhenAll(tasks);
        }

        public static Task<IEnumerable<KeyValuePair<TItem, TResult>>> ForEach<TItem, TResult>(IEnumerable<TItem> input, Func<TItem, Task<TResult>> handler)
        {
            return ForEach(input, handler, CancellationToken.None);
        }

        public static async Task<IEnumerable<KeyValuePair<TItem, TResult>>> ForEach<TItem, TResult>(IEnumerable<TItem> input, Func<TItem, Task<TResult>> handler, CancellationToken cancellationToken)
        {
            var result = new BlockingCollection<KeyValuePair<TItem, TResult>>();
            var tasks = new List<Task>();
            foreach (var item in input)
            {
                tasks.Add(Task.Run(async () => result.Add(new KeyValuePair<TItem, TResult>(item, await handler(item))), cancellationToken));
            }
            await Task.WhenAll(tasks);
            return result.ToList();
        }
    }
}