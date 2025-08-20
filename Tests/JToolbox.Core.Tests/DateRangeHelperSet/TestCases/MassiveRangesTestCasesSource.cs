using JToolbox.Core.Models.DateRanges;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperSet.TestCases
{
    public static class MassiveRangesTestCasesSource
    {
        public static DateTime GetDate(int value) => new DateTime(value, 1, 1);

        public static List<DateRange> GetMassiveRanges1()
        {
            return new List<DateRange>
            {
                GetRange(221, 245),
                GetRange(332, 353),
                GetRange(13, 43),
                GetRange(71, 79),
                GetRange(270, 294),
                GetRange(257, 414),
                GetRange(312, 331),
                GetRange(174, 200),
                GetRange(288, 313),
                GetRange(367, 397),
                GetRange(270, 286),
                GetRange(128, 137),
                GetRange(360, 376),
                GetRange(49, 77),
                GetRange(144, 151),
                GetRange(227, 239),
                GetRange(384, 412),
                GetRange(149, 158),
                GetRange(73, 102),
                GetRange(114, 122),
                GetRange(312, 341),
                GetRange(59, 85),
                GetRange(461, 495),
                GetRange(205, 245),
                GetRange(435, 459),
                GetRange(416, 459),
                GetRange(426, 439),
                GetRange(132, 155),
                GetRange(393, 400),
                GetRange(1, 204)
            };
        }

        public static List<DateRange> GetMassiveRanges2()
        {
            return new List<DateRange>
            {
                GetRange(171, 185),
                GetRange(310, 329),
                GetRange(92, 122),
                GetRange(23, 37),
                GetRange(120, 135),
                GetRange(40, 61),
                GetRange(358, 374),
                GetRange(325, 348),
                GetRange(61, 82),
                GetRange(52, 76),
                GetRange(276, 282),
                GetRange(323, 328),
                GetRange(260, 273),
                GetRange(369, 375),
                GetRange(120, 136),
                GetRange(115, 136),
                GetRange(417, 436),
                GetRange(93, 108),
                GetRange(175, 189),
                GetRange(447, 459),
                GetRange(291, 297),
                GetRange(192, 204),
                GetRange(142, 166),
                GetRange(354, 383),
                GetRange(33, 47),
                GetRange(213, 228),
                GetRange(116, 131),
                GetRange(351, 361),
                GetRange(12, 25),
                GetRange(417, 435),
                GetRange(16, 43),
                GetRange(475, 495),
                GetRange(313, 329),
                GetRange(329, 340),
                GetRange(348, 377),
                GetRange(403, 414),
                GetRange(8, 25),
                GetRange(90, 117),
                GetRange(172, 189),
                GetRange(166, 172)
            };
        }

        public static List<DateRange> GetMassiveRanges3()
        {
            return new List<DateRange>
            {
                GetRange(120, 135),
                GetRange(10, 25),
                GetRange(70, 90),
                GetRange(200, 230),
                GetRange(140, 155),
                GetRange(240, 270),
                GetRange(400, 420),
                GetRange(90, 105),
                GetRange(30, 45),
                GetRange(300, 310),
                GetRange(190, 215),
                GetRange(250, 260),
                GetRange(50, 65),
                GetRange(280, 285),
                GetRange(160, 175),
                GetRange(430, 445),
                GetRange(315, 325),
                GetRange(180, 195),
                GetRange(380, 390),
                GetRange(335, 345),
                GetRange(225, 235),
                GetRange(455, 470),
                GetRange(345, 355),
                GetRange(360, 375),
                GetRange(470, 480),
                GetRange(105, 115),
                GetRange(275, 295),
                GetRange(130, 145),
                GetRange(65, 75),
                GetRange(415, 430),
                GetRange(260, 275),
                GetRange(155, 165),
                GetRange(90, 100),
                GetRange(490, 495),
                GetRange(375, 385),
                GetRange(445, 455),
                GetRange(145, 160),
                GetRange(35, 55),
                GetRange(325, 335),
                GetRange(5, 15),
            };
        }

        public static List<DateRange> GetMassiveRanges4()
        {
            return new List<DateRange>
            {
                GetRange(20, 28),
                GetRange(300, 305),
                GetRange(60, 85),
                GetRange(105, 125),
                GetRange(450, 460),
                GetRange(355, 370),
                GetRange(195, 205),
                GetRange(430, 440),
                GetRange(255, 265),
                GetRange(285, 295),
                GetRange(380, 395),
                GetRange(145, 150),
                GetRange(15, 22),
                GetRange(75, 92),
                GetRange(340, 350),
                GetRange(180, 200),
                GetRange(270, 280),
                GetRange(400, 410),
                GetRange(165, 185),
                GetRange(480, 490),
                GetRange(225, 245),
                GetRange(125, 135),
                GetRange(90, 98),
                GetRange(205, 215),
                GetRange(295, 305),
                GetRange(420, 435),
                GetRange(50, 58),
                GetRange(35, 40),
                GetRange(10, 18),
                GetRange(445, 455),
            };
        }

        public static DateRange GetRange(int from, int to) => new DateRange(GetDate(from), GetDate(to));
    }
}