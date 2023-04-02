using System;

namespace JToolbox.Core.Exceptions
{
    public class StopwatchDidNotStartException : Exception
    {
        public StopwatchDidNotStartException() : base("The stopwatch did not start")
        {
        }
    }
}