using System;

namespace JToolbox.Core.Helpers.Retry
{
    public class RetryResult<TResult>
    {
        public RetryResult(int attempt)
        {
            Attempt = attempt;
        }

        public int Attempt { get; }

        public bool IsCancelled { get; set; }

        public Exception LastException { get; set; }

        public TResult LastResult { get; set; }
    }
}