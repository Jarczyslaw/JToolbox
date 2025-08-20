using JToolbox.Core.Models.DateRanges;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperSet.TestCases.Models
{
    public class TrimTestCase
    {
        public DateTime End { get; set; }

        public List<DateRange> Ranges { get; set; }

        public List<DateRange> Result { get; set; }

        public DateTime Start { get; set; }
    }
}