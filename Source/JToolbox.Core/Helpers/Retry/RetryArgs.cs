using System;
using System.Threading;

namespace JToolbox.Core.Helpers.Retry
{
    public class RetryArgs<TResult>
    {
        public int Attempts { get; set; } = 5;

        public Func<RetryResult<TResult>, bool> BreakHandler { get; set; }

        public CancellationToken CancellationToken { get; set; } = CancellationToken.None;

        public TimeSpan Delay { get; set; } = TimeSpan.Zero;

        public bool Wait(int attempt)
            => attempt < Attempts && Delay > TimeSpan.Zero;
    }
}