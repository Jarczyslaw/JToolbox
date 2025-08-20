using JToolbox.Core.Models.DateRanges;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperSet.TestCases.Models
{
    public class IntersectionTestCase
    {
        public List<DateRange> Ranges1 { get; set; } = new List<DateRange>
        {
            TrimTestCasesSource.GetRange(3, 6),
            TrimTestCasesSource.GetRange(8, 11),
            TrimTestCasesSource.GetRange(13, 16),
        };

        public List<DateRange> Ranges2 { get; set; }

        public List<DateRange> Results { get; set; }
    }
}