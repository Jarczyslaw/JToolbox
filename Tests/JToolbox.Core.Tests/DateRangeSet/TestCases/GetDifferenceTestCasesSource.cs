using JToolbox.Core.Models.DateRanges;
using JToolbox.Core.Tests.DateRangeSet.TestCases.Models;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeSet.TestCases
{
    public static class GetDifferenceTestCasesSource
    {
        public static DateTime GetDate(int value) => new DateTime(1 + value, 1, 1);

        public static List<GetDifferenceTestCase> GetDifferentTestCases()
        {
            return new List<GetDifferenceTestCase>
            {
                new GetDifferenceTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(1, 4),
                    Result = new List<DateRange>
                    {
                        GetRange(6, 10)
                    }
                },
                new GetDifferenceTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(1, 6),
                    Result = new List<DateRange>
                    {
                        GetRange(6, 10)
                    }
                },
                new GetDifferenceTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(3, 7),
                    Result = new List<DateRange>
                    {
                        GetRange(7, 10)
                    }
                },
                new GetDifferenceTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(6, 8),
                    Result = new List<DateRange>
                    {
                        GetRange(8, 10)
                    }
                },
                new GetDifferenceTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(6, 10),
                    Result = new List<DateRange>()
                },
                new GetDifferenceTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(8, 10),
                    Result = new List<DateRange>
                    {
                        GetRange(6, 8)
                    }
                },
                new GetDifferenceTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(8, 12),
                    Result = new List<DateRange>
                    {
                        GetRange(6, 8)
                    }
                },
                new GetDifferenceTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(10, 12),
                    Result = new List<DateRange>
                    {
                        GetRange(6, 10)
                    }
                },
                new GetDifferenceTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(12, 14),
                    Result = new List<DateRange>
                    {
                        GetRange(6, 10)
                    }
                },
                new GetDifferenceTestCase
                {
                    Range1 = GetRange(6, 10),
                    Range2 = GetRange(7, 9),
                    Result = new List<DateRange>
                    {
                        GetRange(6, 7),
                        GetRange(9, 10)
                    }
                },
            };
        }

        public static DateRange GetRange(int from, int to) => new DateRange(GetDate(from), GetDate(to));
    }
}