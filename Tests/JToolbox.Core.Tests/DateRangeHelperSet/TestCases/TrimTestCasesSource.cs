using JToolbox.Core.Models.DateRanges;
using JToolbox.Core.Tests.DateRangeHelperSet.TestCases.Models;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperSet.TestCases
{
    internal static class TrimTestCasesSource
    {
        public static DateTime GetDate(int value) => new DateTime(value, 1, 1);

        public static DateRange GetRange(int from, int to)
            => new DateRange(GetDate(from), GetDate(to));

        public static List<TrimStartOrEndTestCase> TrimEndTestCases() => new List<TrimStartOrEndTestCase>
        {
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(12),
                Result = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(9),
                Result = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(8),
                Result = new List<DateRange>() { GetRange(2, 4), GetRange(7, 8) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(7),
                Result = new List<DateRange>() { GetRange(2, 4) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(5),
                Result = new List<DateRange>() { GetRange(2, 4) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(4),
                Result = new List<DateRange>() { GetRange(2, 4) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(3),
                Result = new List<DateRange>() { GetRange(2, 3) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(1),
                Result = new List<DateRange>()
            }
        };

        public static List<TrimStartOrEndTestCase> TrimStartTestCases() => new List<TrimStartOrEndTestCase>
        {
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(1),
                Result = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(2),
                Result = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(3),
                Result = new List<DateRange>() { GetRange(3, 4), GetRange(7, 9) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(4),
                Result = new List<DateRange>() { GetRange(7, 9) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(5),
                Result = new List<DateRange>() { GetRange(7, 9) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(7),
                Result = new List<DateRange>() { GetRange(7, 9) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(8),
                Result = new List<DateRange>() { GetRange(8, 9) }
            },
            new TrimStartOrEndTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Date = GetDate(9),
                Result = new List<DateRange>()
            }
        };

        public static List<TrimTestCase> TrimTestCases() => new List<TrimTestCase>
        {
            new TrimTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Start = GetDate(1),
                End = GetDate(11),
                Result = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) }
            },
            new TrimTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Start = GetDate(2),
                End = GetDate(9),
                Result = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) }
            },
            new TrimTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Start = GetDate(3),
                End = GetDate(9),
                Result = new List<DateRange>() { GetRange(3, 4), GetRange(7, 9) }
            },
            new TrimTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Start = GetDate(4),
                End = GetDate(9),
                Result = new List<DateRange>() { GetRange(7, 9) }
            },
            new TrimTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Start = GetDate(7),
                End = GetDate(9),
                Result = new List<DateRange>() { GetRange(7, 9) }
            },
            new TrimTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Start = GetDate(8),
                End = GetDate(9),
                Result = new List<DateRange>() { GetRange(8, 9) }
            },
            new TrimTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Start = GetDate(5),
                End = GetDate(6),
                Result = new List<DateRange>()
            },
            new TrimTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Start = GetDate(4),
                End = GetDate(7),
                Result = new List<DateRange>()
            },
            new TrimTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 4), GetRange(7, 9) },
                Start = GetDate(3),
                End = GetDate(8),
                Result = new List<DateRange>() { GetRange(3, 4), GetRange(7, 8) }
            }
        };
    }
}