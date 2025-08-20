using JToolbox.Core.Models.DateRanges;
using JToolbox.Core.Tests.DateRangeHelperSet.TestCases.Models;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperSet.TestCases
{
    public static class GetComplementaryTestCasesSource
    {
        public static List<GetComplementaryTestCase> GetComplementaryTestCases => new List<GetComplementaryTestCase>
        {
            new GetComplementaryTestCase
            {
                Ranges = new List<DateRange>(),
                Results = new List<DateRange>()
            },
            new GetComplementaryTestCase
            {
                Ranges = new List<DateRange>() { GetRange(1, 2) },
                Results = new List<DateRange>()
            },
            new GetComplementaryTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 3), GetRange(1, 4) },
                Results = new List<DateRange>()
            },
            new GetComplementaryTestCase
            {
                Ranges = new List<DateRange>() { GetRange(1, 2), GetRange(2, 3) },
                Results = new List<DateRange>()
            },
            new GetComplementaryTestCase
            {
                Ranges = new List<DateRange>() { GetRange(1, 2), GetRange(4, 5) },
                Results = new List<DateRange>() { GetRange(2, 4) }
            },
            new GetComplementaryTestCase
            {
                Ranges = new List<DateRange>() { GetRange(1, 2), GetRange(4, 5), GetRange(7, 8) },
                Results = new List<DateRange>() { GetRange(2, 4), GetRange(5, 7) }
            },
            new GetComplementaryTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 3), GetRange(1, 5), GetRange(4, 8) },
                Results = new List<DateRange>()
            },
            new GetComplementaryTestCase
            {
                Ranges = new List<DateRange>() { GetRange(2, 3), GetRange(1, 5), GetRange(6, 8), GetRange(8, 10), GetRange(12, 13) },
                Results = new List<DateRange>() { GetRange(5, 6), GetRange(10, 12) }
            },
        };

        public static DateTime GetDate(int value) => new DateTime(value, 1, 1);

        public static DateRange GetRange(int from, int to) => new DateRange(GetDate(from), GetDate(to));
    }
}