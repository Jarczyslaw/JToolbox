using JToolbox.Core.Models;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperTests
{
    internal static class TestCasesSource
    {
        public static List<IntersectionTestCase> IntersectionTestCases => new List<IntersectionTestCase>
        {
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(3, 6), GetRange(8, 11), GetRange(13, 16) },
                Results = new List<DateRange> { GetRange(3, 6), GetRange(8, 11), GetRange(13, 16), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(1, 2), },
                Results = new List<DateRange>()
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(1, 5), },
                Results = new List<DateRange> { GetRange(3, 5) }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(4, 5), GetRange(12, 13), },
                Results = new List<DateRange> { GetRange(4, 5) }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(4, 7), },
                Results = new List<DateRange> { GetRange(4, 6) }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(5, 9), },
                Results = new List<DateRange> { GetRange(5, 6), GetRange(8, 9) }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(9, 10), },
                Results = new List<DateRange> { GetRange(9, 10), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(10, 12), },
                Results = new List<DateRange> { GetRange(10, 11), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(10, 14), },
                Results = new List<DateRange> { GetRange(10, 11), GetRange(13, 14), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(12, 14), },
                Results = new List<DateRange> { GetRange(13, 14), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(14, 15), },
                Results = new List<DateRange> { GetRange(14, 15), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(14, 18), },
                Results = new List<DateRange> { GetRange(14, 16), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(17, 20), },
                Results = new List<DateRange>()
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(2, 9), },
                Results = new List<DateRange> { GetRange(3, 6), GetRange(8, 9), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(7, 15), },
                Results = new List<DateRange> { GetRange(8, 11), GetRange(13, 15), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(3, 16), },
                Results = new List<DateRange> { GetRange(3, 6), GetRange(8, 11), GetRange(13, 16), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(2, 17), },
                Results = new List<DateRange> { GetRange(3, 6), GetRange(8, 11), GetRange(13, 16), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(4, 7), GetRange(9, 12), GetRange(14, 17), },
                Results = new List<DateRange> { GetRange(4, 6), GetRange(9, 11), GetRange(14, 16), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(6, 8), GetRange(11, 13), GetRange(16, 18), },
                Results = new List<DateRange>()
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(7, 9), GetRange(10, 14), GetRange(15, 18), },
                Results = new List<DateRange> { GetRange(8, 9), GetRange(10, 11), GetRange(13, 14), GetRange(15, 16), }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(10, 17), },
                Results = new List<DateRange> { GetRange(10, 11), GetRange(13, 16), }
            },
        };

        public static List<MergeTestCase> MergeTestCases => new List<MergeTestCase>
        {
            new MergeTestCase
            {
                Input = new List<DateRange>(),
                Output = new List<DateRange>()
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 3) },
                Output = new List<DateRange> { GetRange(1, 3) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 3), GetRange(5, 7) },
                Output = new List<DateRange> { GetRange(1, 3), GetRange(5, 7) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 3), GetRange(1, 3) },
                Output = new List<DateRange> { GetRange(1, 3) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 3), GetRange(1, 4), GetRange(1, 3), GetRange(1, 4) },
                Output = new List<DateRange> { GetRange(1, 4) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 4), GetRange(3, 6), GetRange(5, 8) },
                Output = new List<DateRange> { GetRange(1, 8) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 2), GetRange(1, 4), GetRange(1, 3), GetRange(2, 5), GetRange(1, 6) },
                Output = new List<DateRange> { GetRange(1, 6) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 3), GetRange(2, 4), GetRange(5, 7), GetRange(6, 8) },
                Output = new List<DateRange> { GetRange(1, 4), GetRange(5, 8) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 3), GetRange(5, 7), GetRange(6, 8), GetRange(9, 11), GetRange(9, 12), GetRange(9, 10)},
                Output = new List<DateRange> { GetRange(1, 3), GetRange(5, 8), GetRange(9, 12) }
            }
        };

        public static DateRange GetRange(int from, int to)
            => new DateRange(new DateTime(from, 1, 1), new DateTime(to, 1, 1));
    }
}