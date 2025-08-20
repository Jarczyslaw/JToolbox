using JToolbox.Core.Models.DateRanges;
using JToolbox.Core.Tests.DateRangeHelperSet.TestCases.Models;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperSet.TestCases
{
    public static class SplitByDateTestCasesSource
    {
        public static List<SplitByDateTestCase> SplitByDateTestCases => new List<SplitByDateTestCase>
        {
            new SplitByDateTestCase
            {
                Date = GetDate(1),
                Ranges = new List<DateRange> { GetRange(3, 6) },
                LeftSide = new List<DateRange>(),
                RightSide = new List<DateRange>() { GetRange(3, 6) }
            },
            new SplitByDateTestCase
            {
                Date = GetDate(3),
                Ranges = new List<DateRange> { GetRange(3, 6) },
                LeftSide = new List<DateRange>(),
                RightSide = new List<DateRange>() { GetRange(3, 6) }
            },
            new SplitByDateTestCase
            {
                Date = GetDate(4),
                Ranges = new List<DateRange> { GetRange(3, 6) },
                LeftSide = new List<DateRange>() { GetRange(3, 4) },
                RightSide = new List<DateRange>() { GetRange(4, 6) }
            },
            new SplitByDateTestCase
            {
                Date = GetDate(6),
                Ranges = new List<DateRange> { GetRange(3, 6) },
                LeftSide = new List<DateRange>() { GetRange(3, 6) },
                RightSide = new List<DateRange>()
            },
            new SplitByDateTestCase
            {
                Date = GetDate(8),
                Ranges = new List<DateRange> { GetRange(3, 6) },
                LeftSide = new List<DateRange>() { GetRange(3, 6) },
                RightSide = new List<DateRange>()
            },
            new SplitByDateTestCase
            {
                Date = GetDate(5),
                Ranges = new List<DateRange> { GetRange(3, 6), GetRange(4, 7) },
                LeftSide = new List<DateRange>() { GetRange(3, 5), GetRange(4, 5) },
                RightSide = new List<DateRange>() { GetRange(5, 6), GetRange(5, 7) }
            },
        };

        public static DateTime GetDate(int value) => new DateTime(value, 1, 1);

        public static DateRange GetRange(int from, int to) => new DateRange(GetDate(from), GetDate(to));
    }
}