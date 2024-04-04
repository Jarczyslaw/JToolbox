using JToolbox.Core.Exceptions;
using JToolbox.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JToolbox.Core.Utilities
{
    public class ExecutionTime
    {
        private readonly List<ExecutionTimeCheck> _checks = new List<ExecutionTimeCheck>();
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public ExecutionTime(bool start = true)
        {
            if (start) { Start(); }
        }

        public TimeSpan Elapsed
        {
            get
            {
                if (!_stopwatch.IsRunning) { throw new StopwatchDidNotStartException(); }

                return _stopwatch.Elapsed;
            }
        }

        public static TimeSpan RunAction(Action action, string title)
        {
            var stopwatch = Stopwatch.StartNew();
            TimeSpan elapsed;
            try
            {
                action();
            }
            finally
            {
                elapsed = FinishStopwatch(stopwatch, title);
            }

            return elapsed;
        }

        public static async Task<TimeSpan> RunActionAsync(Func<Task> action, string title)
        {
            var stopwatch = Stopwatch.StartNew();
            TimeSpan elapsed;
            try
            {
                await action();
            }
            finally
            {
                elapsed = FinishStopwatch(stopwatch, title);
            }

            return elapsed;
        }

        public static T RunFunc<T>(Func<T> func, string title)
            => RunFunc(func, title, out TimeSpan _);

        public static T RunFunc<T>(Func<T> func, string title, out TimeSpan elapsed)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                return func();
            }
            finally
            {
                elapsed = FinishStopwatch(stopwatch, title);
            }
        }

        public static async Task<T> RunFuncAsync<T>(Func<Task<T>> func, string title)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                return await func();
            }
            finally
            {
                FinishStopwatch(stopwatch, title);
            }
        }

        public void Check(string title, bool restart = true)
        {
            if (!_stopwatch.IsRunning) { throw new StopwatchDidNotStartException(); }

            Stop();

            _checks.Add(new ExecutionTimeCheck
            {
                Title = title,
                Elapsed = _stopwatch.Elapsed
            });

            if (restart) { Start(); }
        }

        public void ClearChecks() => _checks.Clear();

        public string GetChecksResult(bool clearChecks = true, bool orderByElapsedTime = false)
        {
            Stop();

            if (_checks.Count == 0) { return string.Empty; }

            var checks = orderByElapsedTime
                ? _checks.OrderByDescending(x => x.Elapsed).ToList()
                : _checks.ToList();
            var format = _checks.Max(x => x.Elapsed).GetTimeSpanFormat();

            if (clearChecks) { _checks.Clear(); }

            var sb = new StringBuilder();
            sb.AppendLine($"{nameof(ExecutionTime).ToUpper()} - Summary:");

            for (int i = 0; i < checks.Count; i++)
            {
                var check = checks[i];
                sb.AppendLine($"{i + 1}. {check.Title}:\t{check.Elapsed.GetTimeSpanFormattedString(format)}");
            }

            var elapsedSum = checks.Select(x => x.Elapsed).Sum();
            sb.AppendLine($"Summary: {elapsedSum.GetTimeSpanFormattedString(format)}");

            var result = sb.ToString();

            Debug.WriteLine(result);

            return result;
        }

        public void Start() => _stopwatch.Restart();

        public void Stop() => _stopwatch.Stop();

        private static TimeSpan FinishStopwatch(Stopwatch stopwatch, string title)
        {
            stopwatch.Stop();

            ShowResult(title, stopwatch.Elapsed);

            return stopwatch.Elapsed;
        }

        private static void ShowResult(string title, TimeSpan elapsed)
        {
            var timeElapsed = elapsed.GetTimeSpanFormattedString();
            Debug.WriteLine($"{nameof(ExecutionTime).ToUpper()} - {title} elapsed: " + timeElapsed);
        }

        private class ExecutionTimeCheck
        {
            public TimeSpan Elapsed { get; set; }

            public string Title { get; set; }
        }
    }
}