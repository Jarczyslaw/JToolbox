using JToolbox.Core.Models.DateRanges;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperSet.TestCases.Models
{
    public class TrimStartOrEndTestCase
    {
        public DateTime Date { get; set; }

        public List<DateRange> Ranges { get; set; }

        public List<DateRange> Result { get; set; }
    }
}