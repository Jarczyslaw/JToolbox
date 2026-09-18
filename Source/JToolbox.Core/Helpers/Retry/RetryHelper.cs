using System;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Core.Helpers.Retry
{
    public static class RetryHelper
    {
        public static RetryResult<TResult> Try<TResult>(
            Func<TResult> action,
            RetryArgs<TResult> args)
        {
            RetryResult<TResult> retryResult = null;

            for (int attempt = 1; attempt <= args.Attempts; attempt++)
            {
                retryResult = new RetryResult<TResult>(attempt);

                if (args.CancellationToken.IsCancellationRequested)
                {
                    retryResult.IsCancelled = true;
                    return retryResult;
                }

                try
                {
                    retryResult.LastResult = action();
                }
                catch (Exception ex)
                {
                    retryResult.LastException = ex;
                }

                if (args.BreakHandler(retryResult)) { return retryResult; }

                if (args.Wait(attempt)) { Thread.Sleep(args.Delay); }
            }

            return retryResult;
        }

        public static async Task<RetryResult<TResult>> TryAsync<TResult>(
            Func<Task<TResult>> action,
            RetryArgs<TResult> args)
        {
            RetryResult<TResult> retryResult = null;

            for (int attempt = 1; attempt <= args.Attempts; attempt++)
            {
                retryResult = new RetryResult<TResult>(attempt);

                if (args.CancellationToken.IsCancellationRequested)
                {
                    retryResult.IsCancelled = true;
                    return retryResult;
                }

                try
                {
                    retryResult.LastResult = await action();
                }
                catch (Exception ex)
                {
                    retryResult.LastException = ex;
                }

                if (args.BreakHandler(retryResult)) { return retryResult; }

                if (args.Wait(attempt))
                {
                    try
                    {
                        await Task.Delay(args.Delay, args.CancellationToken);
                    }
                    catch (TaskCanceledException) { }
                }
            }

            return retryResult;
        }
    }
}