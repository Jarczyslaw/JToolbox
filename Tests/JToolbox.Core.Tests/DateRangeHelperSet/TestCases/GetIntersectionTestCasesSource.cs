using JToolbox.Core.Models.DateRanges;
using JToolbox.Core.Tests.DateRangeHelperSet.TestCases.Models;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperSet.TestCases
{
    public static class GetIntersectionTestCasesSource
    {
        public static List<IntersectionTestCase> GetIntersectionTestCases => new List<IntersectionTestCase>
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
                Ranges2 = new List<DateRange> { GetRange(7, 9), GetRange(9, 10) },
                Results = new List<DateRange> { GetRange(8, 10) }
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
                Ranges2 = new List<DateRange> { GetRange(7, 9), GetRange(7, 8), GetRange(12, 14), GetRange(12, 15)},
                Results = new List<DateRange> { GetRange(8, 9), GetRange(13, 15) }
            },
            new IntersectionTestCase
            {
                Ranges2 = new List<DateRange> { GetRange(10, 17), },
                Results = new List<DateRange> { GetRange(10, 11), GetRange(13, 16), }
            },
            new IntersectionTestCase
            {
                Ranges1 = new List<DateRange>
                {
                    GetRange(1,5),
                    GetRange(6,10),
                    GetRange(11,15),
                    GetRange(16,18),
                    GetRange(20,25),
                    GetRange(27,30),
                    GetRange(32,35),
                    GetRange(36,40),
                    GetRange(42,45),
                    GetRange(47,50)
                },
                Ranges2 = new List<DateRange>
                {
                    GetRange(1,4),
                    GetRange(5,7),
                    GetRange(8,12),
                    GetRange(13,17),
                    GetRange(19,22),
                    GetRange(23,26),
                    GetRange(28,33),
                    GetRange(34,37),
                    GetRange(41,44),
                    GetRange(48,52)
                },
                Results = new List<DateRange>
                {
                    GetRange(1, 4),
                    GetRange(6, 7),
                    GetRange(8, 10),
                    GetRange(11, 12),
                    GetRange(13, 15),
                    GetRange(16, 17),
                    GetRange(20, 22),
                    GetRange(23, 25),
                    GetRange(28, 30),
                    GetRange(32, 33),
                    GetRange(34, 35),
                    GetRange(36, 37),
                    GetRange(42, 44),
                    GetRange(48, 50)
                }
            },
            new IntersectionTestCase
            {
                Ranges1 = MassiveRangesTestCasesSource.GetMassiveRanges1(),
                Ranges2 = MassiveRangesTestCasesSource.GetMassiveRanges2(),
                Results = new List<DateRange>
                {
                    GetRange(8, 82),
                    GetRange(90, 136),
                    GetRange(142, 189),
                    GetRange(192, 204),
                    GetRange(213, 228),
                    GetRange(260, 273),
                    GetRange(276, 282),
                    GetRange(291, 297),
                    GetRange(310, 383),
                    GetRange(403, 414),
                    GetRange(417, 436),
                    GetRange(447, 459),
                    GetRange(475, 495)
                }
            },
            new IntersectionTestCase
            {
                Ranges1 = MassiveRangesTestCasesSource.GetMassiveRanges3(),
                Ranges2 = MassiveRangesTestCasesSource.GetMassiveRanges4(),
                Results = new List<DateRange>
                {
                    GetRange(10, 25),
                    GetRange(35, 40),
                    GetRange(50, 58),
                    GetRange(60, 98),
                    GetRange(105, 115),
                    GetRange(120, 135),
                    GetRange(145, 150),
                    GetRange(165, 175),
                    GetRange(180, 215),
                    GetRange(225, 235),
                    GetRange(240, 245),
                    GetRange(255, 265),
                    GetRange(270, 280),
                    GetRange(285, 295),
                    GetRange(300, 305),
                    GetRange(340, 350),
                    GetRange(360, 370),
                    GetRange(380, 390),
                    GetRange(400, 410),
                    GetRange(420, 440),
                    GetRange(445, 460),
                }
            }
        };

        public static DateTime GetDate(int value) => new DateTime(value, 1, 1);

        public static DateRange GetRange(int from, int to) => new DateRange(GetDate(from), GetDate(to));
    }
}