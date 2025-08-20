using JToolbox.Core.Models.DateRanges;
using JToolbox.Core.Tests.DateRangeHelperSet.TestCases.Models;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperSet.TestCases
{
    public static class GetDifferenceTestCasesSource
    {
        public static List<GetDifferenceTestCase> GetDifferenceTestCases => new List<GetDifferenceTestCase>
        {
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>(),
                Ranges2 = new List<DateRange>(),
                Results = new List<DateRange>()
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(2, 5), GetRange(9, 12) },
                Ranges2 = new List<DateRange>(),
                Results = new List<DateRange>() { GetRange(2, 5), GetRange(9, 12) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(2, 5), GetRange(9, 12) },
                Ranges2 = new List<DateRange>() { GetRange(2, 12) },
                Results = new List<DateRange>()
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(2, 12) },
                Ranges2 = new List<DateRange>() { GetRange(2, 6), GetRange(6, 12) },
                Results = new List<DateRange>()
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>(),
                Ranges2 = new List<DateRange>() { GetRange(2, 5), GetRange(9, 12) },
                Results = new List<DateRange>()
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(2, 5), GetRange(9, 12) },
                Ranges2 = new List<DateRange>() { GetRange(2, 5), GetRange(9, 12) },
                Results = new List<DateRange>()
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(2, 5), GetRange(9, 12) },
                Ranges2 = new List<DateRange>() { GetRange(2, 5), GetRange(9, 12), GetRange(14, 15) },
                Results = new List<DateRange>()
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(2, 10) },
                Ranges2 = new List<DateRange>() { GetRange(3, 4), GetRange(7, 8) },
                Results = new List<DateRange>() { GetRange(2, 3), GetRange(4, 7), GetRange(8, 10) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(2, 10) },
                Ranges2 = new List<DateRange>() { GetRange(1, 3), GetRange(9, 11) },
                Results = new List<DateRange>() { GetRange(3, 9) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(2, 10) },
                Ranges2 = new List<DateRange>() { GetRange(1, 2), GetRange(10, 11) },
                Results = new List<DateRange>() { GetRange(2, 10) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(6, 10) },
                Ranges2 = new List<DateRange>() { GetRange(1, 2), GetRange(3, 4), GetRange(11, 13) },
                Results = new List<DateRange>() { GetRange(6, 10) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(3, 6), GetRange(8, 10) },
                Ranges2 = new List<DateRange>() { GetRange(6, 8) },
                Results = new List<DateRange>() { GetRange(3, 6), GetRange(8, 10) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(3, 6), GetRange(8, 10) },
                Ranges2 = new List<DateRange>() { GetRange(5, 9) },
                Results = new List<DateRange>() { GetRange(3, 5), GetRange(9, 10) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(3, 6), GetRange(8, 10), GetRange(12, 14), GetRange(15, 17) },
                Ranges2 = new List<DateRange>() { GetRange(5, 9), GetRange(12, 13) },
                Results = new List<DateRange>() { GetRange(3, 5), GetRange(9, 10), GetRange(13, 14), GetRange(15, 17) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>()
                {
                    GetRange(1, 10),
                    GetRange(5, 15),
                    GetRange(20, 30),
                    GetRange(28, 35),
                    GetRange(40, 50),
                    GetRange(48, 55),
                    GetRange(60, 70),
                    GetRange(68, 75),
                    GetRange(80, 85),
                    GetRange(90, 100),
                    GetRange(105, 110),
                    GetRange(112, 120),
                    GetRange(125, 135),
                    GetRange(137, 145),
                    GetRange(150, 160),
                    GetRange(162, 170),
                    GetRange(175, 180),
                    GetRange(182, 190),
                    GetRange(195, 200),
                    GetRange(205, 210),
                    GetRange(215, 225),
                    GetRange(230, 240),
                    GetRange(245, 250),
                    GetRange(255, 260),
                    GetRange(262, 270),
                    GetRange(275, 280),
                    GetRange(285, 290),
                    GetRange(295, 300),
                    GetRange(305, 310),
                    GetRange(312, 320),
                    GetRange(325, 330),
                    GetRange(335, 340),
                    GetRange(342, 350),
                    GetRange(355, 360),
                    GetRange(362, 370),
                    GetRange(375, 380),
                    GetRange(385, 390),
                    GetRange(395, 400),
                    GetRange(402, 410),
                    GetRange(412, 420)
                },
                Ranges2 = new List<DateRange>()
                {
                    GetRange(3, 8),
                    GetRange(10, 12),
                    GetRange(25, 32),
                    GetRange(34, 36),
                    GetRange(45, 52),
                    GetRange(65, 72),
                    GetRange(85, 95),
                    GetRange(98, 108),
                    GetRange(113, 118),
                    GetRange(122, 127),
                    GetRange(130, 132),
                    GetRange(140, 148),
                    GetRange(151, 158),
                    GetRange(163, 165),
                    GetRange(178, 185),
                    GetRange(192, 198),
                    GetRange(207, 212),
                    GetRange(218, 220),
                    GetRange(233, 237),
                    GetRange(248, 258),
                    GetRange(263, 268),
                    GetRange(276, 278),
                    GetRange(288, 292),
                    GetRange(296, 297),
                    GetRange(308, 315),
                    GetRange(328, 332),
                    GetRange(336, 338),
                    GetRange(345, 348),
                    GetRange(357, 368),
                    GetRange(388, 392)
                },
                Results = new List<DateRange>()
                {
                    GetRange(1, 3),
                    GetRange(8, 10),
                    GetRange(12, 15),
                    GetRange(20, 25),
                    GetRange(32, 34),
                    GetRange(40, 45),
                    GetRange(52, 55),
                    GetRange(60, 65),
                    GetRange(72, 75),
                    GetRange(80, 85),
                    GetRange(95, 98),
                    GetRange(108, 110),
                    GetRange(112, 113),
                    GetRange(118, 120),
                    GetRange(127, 130),
                    GetRange(132, 135),
                    GetRange(137, 140),
                    GetRange(150, 151),
                    GetRange(158, 160),
                    GetRange(162, 163),
                    GetRange(165, 170),
                    GetRange(175, 178),
                    GetRange(185, 190),
                    GetRange(198, 200),
                    GetRange(205, 207),
                    GetRange(215, 218),
                    GetRange(220, 225),
                    GetRange(230, 233),
                    GetRange(237, 240),
                    GetRange(245, 248),
                    GetRange(258, 260),
                    GetRange(262, 263),
                    GetRange(268, 270),
                    GetRange(275, 276),
                    GetRange(278, 280),
                    GetRange(285, 288),
                    GetRange(295, 296),
                    GetRange(297, 300),
                    GetRange(305, 308),
                    GetRange(315, 320),
                    GetRange(325, 328),
                    GetRange(335, 336),
                    GetRange(338, 340),
                    GetRange(342, 345),
                    GetRange(348, 350),
                    GetRange(355, 357),
                    GetRange(368, 370),
                    GetRange(375, 380),
                    GetRange(385, 388),
                    GetRange(395, 400),
                    GetRange(402, 410),
                    GetRange(412, 420),
                }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) },
                Ranges2 = new List<DateRange>() { GetRange(1, 4), GetRange(6, 9) },
                Results = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) },
                Ranges2 = new List<DateRange>() { GetRange(3, 6), GetRange(8, 11) },
                Results = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) },
                Ranges2 = new List<DateRange>() { GetRange(4, 7), GetRange(9, 12) },
                Results = new List<DateRange>() { GetRange(12, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) },
                Ranges2 = new List<DateRange>() { GetRange(5, 8), GetRange(10, 13) },
                Results = new List<DateRange>() { GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) },
                Ranges2 = new List<DateRange>() { GetRange(7, 10), GetRange(12, 15) },
                Results = new List<DateRange>() { GetRange(11, 12), GetRange(15, 16), GetRange(17, 19), GetRange(20, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) },
                Ranges2 = new List<DateRange>() { GetRange(11, 14), GetRange(16, 19) },
                Results = new List<DateRange>() { GetRange(14, 16), GetRange(20, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) },
                Ranges2 = new List<DateRange>() { GetRange(12, 15), GetRange(17, 20) },
                Results = new List<DateRange>() { GetRange(11, 12), GetRange(15, 16), GetRange(20, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) },
                Ranges2 = new List<DateRange>() { GetRange(13, 16), GetRange(18, 21) },
                Results = new List<DateRange>() { GetRange(11, 13), GetRange(17, 18), GetRange(21, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) },
                Ranges2 = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) },
                Results = new List<DateRange>() { GetRange(11, 13), GetRange(17, 19) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) },
                Ranges2 = new List<DateRange>() { GetRange(16, 19), GetRange(21, 24) },
                Results = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(20, 21) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) },
                Ranges2 = new List<DateRange>() { GetRange(20, 23), GetRange(25, 28) },
                Results = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) },
                Ranges2 = new List<DateRange>() { GetRange(22, 25), GetRange(27, 30) },
                Results = new List<DateRange>() { GetRange(11, 13), GetRange(14, 16), GetRange(17, 19), GetRange(20, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) },
                Ranges2 = new List<DateRange>() { GetRange(2, 4), GetRange(5, 7), GetRange(8, 10), GetRange(11, 13) },
                Results = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) },
                Ranges2 = new List<DateRange>() { GetRange(3, 5), GetRange(6, 8), GetRange(9, 11), GetRange(12, 14) },
                Results = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) },
                Ranges2 = new List<DateRange>() { GetRange(5, 7), GetRange(8, 10), GetRange(11, 13), GetRange(14, 16) },
                Results = new List<DateRange>() { GetRange(16, 17), GetRange(19, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) },
                Ranges2 = new List<DateRange>() { GetRange(7, 9), GetRange(10, 12), GetRange(13, 15), GetRange(16, 18) },
                Results = new List<DateRange>() { GetRange(15, 16), GetRange(19, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) },
                Ranges2 = new List<DateRange>() { GetRange(10, 12), GetRange(13, 15), GetRange(16, 18), GetRange(19, 21) },
                Results = new List<DateRange>() { GetRange(15, 16), GetRange(21, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) },
                Ranges2 = new List<DateRange>() { GetRange(13, 15), GetRange(16, 18), GetRange(19, 21), GetRange(22, 24) },
                Results = new List<DateRange>() { GetRange(15, 16), GetRange(21, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) },
                Ranges2 = new List<DateRange>() { GetRange(17, 19), GetRange(20, 22), GetRange(23, 25), GetRange(26, 28) },
                Results = new List<DateRange>() { GetRange(14, 17), GetRange(19, 20) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) },
                Ranges2 = new List<DateRange>() { GetRange(19, 21), GetRange(22, 24), GetRange(25, 27), GetRange(28, 30) },
                Results = new List<DateRange>() { GetRange(14, 17), GetRange(21, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) },
                Ranges2 = new List<DateRange>() { GetRange(23, 25), GetRange(26, 28), GetRange(29, 31), GetRange(32, 34) },
                Results = new List<DateRange>() { GetRange(14, 17), GetRange(19, 22) }
            },
            new GetDifferenceTestCase
            {
                Ranges1 = MassiveRangesTestCasesSource.GetMassiveRanges3(),
                Ranges2 = MassiveRangesTestCasesSource.GetMassiveRanges4(),
                Results = new List<DateRange>
                {
                    GetRange(5, 10),
                    GetRange(30, 35),
                    GetRange(40, 50),
                    GetRange(58, 60),
                    GetRange(98, 105),
                    GetRange(135, 145),
                    GetRange(150, 165),
                    GetRange(215, 225),
                    GetRange(245, 255),
                    GetRange(265, 270),
                    GetRange(280, 285),
                    GetRange(305, 310),
                    GetRange(315, 340),
                    GetRange(350, 355),
                    GetRange(370, 380),
                    GetRange(410, 420),
                    GetRange(440, 445),
                    GetRange(460, 480),
                    GetRange(490, 495),
                }
            }
        };

        public static DateTime GetDate(int value) => new DateTime(value, 1, 1);

        public static DateRange GetRange(int from, int to) => new DateRange(GetDate(from), GetDate(to));
    }
}