using System;
using System.Diagnostics;

namespace JToolbox.Core.Utilities
{
    public static class ExecutionTime
    {
        private static Stopwatch stopwatch;

        public static void Run(Action action, out TimeSpan elapsed)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                action();
            }
            finally
            {
                stopwatch.Stop();
                elapsed = stopwatch.Elapsed;
            }
        }

        public static T Run<T>(Func<T> func, out TimeSpan elapsed)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                return func();
            }
            finally
            {
                stopwatch.Stop();
                elapsed = stopwatch.Elapsed;
            }
        }

        public static void Start() => stopwatch = Stopwatch.StartNew();

        public static TimeSpan Stop()
        {
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}