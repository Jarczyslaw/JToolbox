using System;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Core.Helpers.Retry
{
    public static class RetryHelper
    {
        public static RetryResult<TResult> TryUntilSuccess<TResult>(
            Func<TResult> action,
            RetryArgs<TResult> args)
        {
            RetryResult<TResult> retryResult = new RetryResult<TResult>();

            for (int attempt = 1; attempt <= args.Attempts; attempt++)
            {
                retryResult.InitializeAttempt(attempt);

                if (args.CancellationToken.IsCancellationRequested)
                {
                    retryResult.IsCancelled = true;
                    return retryResult;
                }

                try
                {
                    retryResult.LastResult = action();

                    if (!args.RetryPredicate(retryResult.LastResult))
                    {
                        retryResult.IsSuccess = true;
                        return retryResult;
                    }
                }
                catch (Exception ex)
                {
                    retryResult.LastException = ex;
                }

                args.FailAction?.Invoke(retryResult);

                if (args.Wait(attempt)) { Thread.Sleep(args.Delay); }
            }

            return retryResult;
        }

        public static async Task<RetryResult<TResult>> TryUntilSuccessAsync<TResult>(
            Func<Task<TResult>> action,
            RetryArgs<TResult> args)
        {
            RetryResult<TResult> retryResult = new RetryResult<TResult>();

            for (int attempt = 1; attempt <= args.Attempts; attempt++)
            {
                retryResult.InitializeAttempt(attempt);

                if (args.CancellationToken.IsCancellationRequested)
                {
                    retryResult.IsCancelled = true;
                    return retryResult;
                }

                try
                {
                    retryResult.LastResult = await action();

                    if (!args.RetryPredicate(retryResult.LastResult))
                    {
                        retryResult.IsSuccess = true;
                        return retryResult;
                    }
                }
                catch (Exception ex)
                {
                    retryResult.LastException = ex;
                }

                args.FailAction?.Invoke(retryResult);

                if (args.Wait(attempt)) { await Task.Delay(args.Delay, args.CancellationToken); }
            }

            return retryResult;
        }
    }
}