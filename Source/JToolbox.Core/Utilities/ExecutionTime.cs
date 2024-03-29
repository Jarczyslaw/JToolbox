﻿using JToolbox.Core.Exceptions;
using JToolbox.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JToolbox.Core.Utilities
{
    public static class ExecutionTime
    {
        private static readonly List<ExecutionTimeCheck> _checks = new List<ExecutionTimeCheck>();
        private static Stopwatch _stopwatch;

        public static TimeSpan Elapsed
        {
            get
            {
                if (_stopwatch == null) { throw new StopwatchDidNotStartException(); }

                return _stopwatch.Elapsed;
            }
        }

        public static void Check(string title, bool startNew = true)
        {
            _checks.Add(new ExecutionTimeCheck
            {
                Title = title,
                Elapsed = FinishStopwatch(_stopwatch, false)
            });

            if (startNew) { Start(); }
        }

        public static void ClearChecks() => _checks.Clear();

        public static string GetChecksResult(bool clearChecks = true, bool orderByElapsedTime = false)
        {
            if (_checks.Count == 0) { return "NO CHECKS"; }

            var checks = orderByElapsedTime
                ? _checks.OrderByDescending(x => x.Elapsed).ToList()
                : _checks.ToList();
            var format = _checks.Max(x => x.Elapsed).GetTimeSpanFormat();

            if (clearChecks) { _checks.Clear(); }

            var sb = new StringBuilder();
            sb.AppendLine("Stopwatch summary:");

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

        public static TimeSpan RunAction(Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            TimeSpan elapsed;
            try
            {
                action();
            }
            finally
            {
                elapsed = FinishStopwatch(stopwatch);
            }

            return elapsed;
        }

        public static async Task<TimeSpan> RunAction(Func<Task> action)
        {
            var stopwatch = Stopwatch.StartNew();
            TimeSpan elapsed;
            try
            {
                await action();
            }
            finally
            {
                elapsed = FinishStopwatch(stopwatch);
            }

            return elapsed;
        }

        public static T RunAction<T>(Func<T> action, out TimeSpan elapsed)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                return action();
            }
            finally
            {
                elapsed = FinishStopwatch(stopwatch);
            }
        }

        public static void Start() => _stopwatch = Stopwatch.StartNew();

        public static TimeSpan Stop() => FinishStopwatch(_stopwatch);

        private static TimeSpan FinishStopwatch(Stopwatch stopwatch, bool display = true)
        {
            if (stopwatch == null) { throw new StopwatchDidNotStartException(); }

            stopwatch.Stop();

            if (display) { ShowResult(stopwatch.Elapsed); }

            return stopwatch.Elapsed;
        }

        private static void ShowResult(TimeSpan elapsed)
        {
            var timeElapsed = elapsed.GetTimeSpanFormattedString();
            Debug.WriteLine($"Elapsed: " + timeElapsed);
        }

        private class ExecutionTimeCheck
        {
            public TimeSpan Elapsed { get; set; }

            public string Title { get; set; }
        }
    }
}