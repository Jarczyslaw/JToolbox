using JToolbox.Core.Models.DateRanges;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeSet.TestCases.Models
{
    public class GetDifferenceTestCase
    {
        public DateRange Range1 { get; set; }

        public DateRange Range2 { get; set; }

        public List<DateRange> Result { get; set; }
    }
}