using JToolbox.Core.Models.DateRanges;
using JToolbox.Core.Tests.DateRangeSet.TestCases.Models;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeSet.TestCases
{
    public static class GetIntersectionTestCasesSource
    {
        public static DateTime GetDate(int value) => new DateTime(1 + value, 1, 1);

        public static List<MergeTestCase> GetIntersectionTestCases()
        {
            return new List<MergeTestCase>
            {
                new MergeTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(1, 4),
                    Result = null
                },
                new MergeTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(1, 6),
                    Result = null
                },
                new MergeTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(3, 7),
                    Result = GetRange(6, 7)
                },
                new MergeTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(6, 8),
                    Result = GetRange(6, 8)
                },
                new MergeTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(6, 10),
                    Result = GetRange(6, 10)
                },
                new MergeTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(8, 10),
                    Result = GetRange(8, 10)
                },
                new MergeTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(8, 12),
                    Result = GetRange(8, 10)
                },
                new MergeTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(10, 12),
                    Result = null
                },
                new MergeTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(12, 14),
                    Result = null
                },
                new MergeTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(7, 9),
                    Result = GetRange(7, 9)
                },
            };
        }

        public static DateRange GetRange(int from, int to) => new DateRange(GetDate(from), GetDate(to));
    }
}