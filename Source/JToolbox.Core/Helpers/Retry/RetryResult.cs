using System;

namespace JToolbox.Core.Helpers.Retry
{
    public class RetryResult<TResult>
    {
        public int Attempt { get; set; }

        public bool IsCancelled { get; set; }

        public bool IsSuccess { get; set; }

        public Exception LastException { get; set; }

        public TResult LastResult { get; set; }

        public void InitializeAttempt(int attempt)
        {
            Attempt = attempt;
            LastException = null;
            LastResult = default;
        }
    }
}