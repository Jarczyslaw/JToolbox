using System;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Core.Helpers
{
    public static class SyncExecutor
    {
        private static readonly TaskFactory _taskFactory = new TaskFactory(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default);

        public static TResult Run<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken = default)
            => _taskFactory
                .StartNew(func, cancellationToken)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        public static void Run(Func<Task> func, CancellationToken cancellationToken = default)
            => _taskFactory
                .StartNew(func, cancellationToken)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
    }
}