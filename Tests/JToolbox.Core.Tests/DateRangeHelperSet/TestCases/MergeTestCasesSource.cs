using JToolbox.Core.Models.DateRanges;
using JToolbox.Core.Tests.DateRangeHelperSet.TestCases.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Tests.DateRangeHelperSet.TestCases
{
    public static class MergeTestCasesSource
    {
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
                Input = new List<DateRange> { GetRange(1, 3), GetRange(5, 7), GetRange(9, 10) },
                Output = new List<DateRange> { GetRange(1, 3), GetRange(5, 7), GetRange(9, 10) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 3), GetRange(1, 3) },
                Output = new List<DateRange> { GetRange(1, 3) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 3), GetRange(3, 5) },
                Output = new List<DateRange> { GetRange(1, 5) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 2), GetRange(1, 3), GetRange(1, 4), GetRange(2, 5) },
                Output = new List<DateRange> { GetRange(1, 5) }
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
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 3), GetRange(6, 8), GetRange(11, 16), GetRange(16, 17)},
                Output = new List<DateRange> { GetRange(1, 3), GetRange(6, 8), GetRange(11, 17) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 3), GetRange(6, 8), GetRange(11, 16), GetRange(16, 17)},
                Output = new List<DateRange> { GetRange(1, 3), GetRange(6, 8), GetRange(11, 17) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange> { GetRange(1, 3), GetRange(2, 4), GetRange(5, 6), GetRange(6, 7), GetRange(5, 8), GetRange(9, 10)},
                Output = new List<DateRange> { GetRange(1, 4), GetRange(5, 8), GetRange(9, 10) }
            },
            new MergeTestCase
            {
                Input = new List<DateRange>
                {
                    GetRange(1, 5),
                    GetRange(3, 7),
                    GetRange(6, 9),
                    GetRange(10, 12),
                    GetRange(36, 38),
                    GetRange(11, 14),
                    GetRange(17, 20),
                    GetRange(21, 23),
                    GetRange(60, 65),
                    GetRange(22, 25),
                    GetRange(26, 28),
                    GetRange(30, 31),
                    GetRange(32, 35),
                    GetRange(34, 36),
                    GetRange(40, 42),
                    GetRange(41, 45),
                    GetRange(43, 44),
                    GetRange(46, 48),
                    GetRange(47, 49),
                    GetRange(15, 18),
                },
                Output = new List<DateRange>
                {
                    GetRange(1, 9),
                    GetRange(10, 14),
                    GetRange(15, 20),
                    GetRange(21, 25),
                    GetRange(26, 28),
                    GetRange(30, 31),
                    GetRange(32, 38),
                    GetRange(40, 45),
                    GetRange(46, 49),
                    GetRange(60, 65),
                }
            },
            new MergeTestCase
            {
                Input = MassiveRangesTestCasesSource.GetMassiveRanges1().Concat(MassiveRangesTestCasesSource.GetMassiveRanges2()).ToList(),
                Output = new List<DateRange>
                {
                    GetRange(1, 204),
                    GetRange(205, 245),
                    GetRange(257, 414),
                    GetRange(416, 459),
                    GetRange(461, 495)
                }
            },
            new MergeTestCase
            {
                Input = MassiveRangesTestCasesSource.GetMassiveRanges3().Concat(MassiveRangesTestCasesSource.GetMassiveRanges4()).ToList(),
                Output = new List<DateRange>
                {
                    GetRange(5, 28),
                    GetRange(30, 310),
                    GetRange(315, 395),
                    GetRange(400, 495),
                }
            }
        };

        public static DateTime GetDate(int value) => new DateTime(value, 1, 1);

        public static DateRange GetRange(int from, int to) => new DateRange(GetDate(from), GetDate(to));
    }
}