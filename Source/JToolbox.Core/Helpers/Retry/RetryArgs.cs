using System;
using System.Threading;

namespace JToolbox.Core.Helpers.Retry
{
    public class RetryArgs<TResult>
    {
        public int Attempts { get; set; } = 5;

        public CancellationToken CancellationToken { get; set; } = CancellationToken.None;

        public TimeSpan Delay { get; set; } = TimeSpan.Zero;

        public Action<RetryResult<TResult>> FailAction { get; set; }

        public Func<TResult, bool> RetryPredicate { get; set; }

        public bool Wait(int attempt)
            => attempt < Attempts && Delay > TimeSpan.Zero;
    }
}