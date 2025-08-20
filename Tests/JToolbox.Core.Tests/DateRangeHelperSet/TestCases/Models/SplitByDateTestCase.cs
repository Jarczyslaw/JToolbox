using JToolbox.Core.Models.DateRanges;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperSet.TestCases.Models
{
    public class SplitByDateTestCase
    {
        public DateTime Date { get; set; }

        public List<DateRange> LeftSide { get; set; }

        public List<DateRange> Ranges { get; set; }

        public List<DateRange> RightSide { get; set; }
    }
}