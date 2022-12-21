using JToolbox.Core.Models;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperTests
{
    public class IntersectionTestCase
    {
        public List<DateRange> Ranges1 { get; set; } = new List<DateRange>
        {
            TestCasesSource.GetRange(3, 6),
            TestCasesSource.GetRange(8, 11),
            TestCasesSource.GetRange(13, 16),
        };

        public List<DateRange> Ranges2 { get; set; }

        public List<DateRange> Results { get; set; }
    }
}