using JToolbox.Core.Extensions;
using JToolbox.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Helpers
{
    public static class DateRangeHelper
    {
        public static List<DateRange> Intersection(
            List<DateRange> ranges1,
            List<DateRange> ranges2,
            bool includeBoundaries)
        {
            if (ranges1.None() || ranges2.None()) { return new List<DateRange>(); }

            var orderedRanges1 = OrderByStartAndCheckOverlapping(ranges1);
            var orderedRanges2 = OrderByStartAndCheckOverlapping(ranges2);

            var allRanges = orderedRanges1.Concat(orderedRanges2)
                .OrderBy(x => x.Start)
                .ToList();

            var result = new List<DateRange>();

            var previousRange = allRanges[0];
            for (int i = 1; i < allRanges.Count; i++)
            {
                var currentRange = allRanges[i];
                var intersection = currentRange.Intersection(previousRange, includeBoundaries);
                if (intersection != null)
                {
                    result.Add(intersection);
                }

                if (currentRange.End > previousRange.End)
                {
                    previousRange = currentRange;
                }
            }

            return result;
        }

        public static List<DateRange> OrderByStartAndCheckOverlapping(List<DateRange> ranges)
        {
            var result = ranges.OrderBy(x => x.Start)
                .ToList();

            if (result.Count > 1)
            {
                for (int i = 1; i < result.Count; i++)
                {
                    if (result[i - 1].End > result[i].Start)
                    {
                        throw new ArgumentException("Input ranges overlap internally");
                    }
                }
            }
            return result;
        }
    }
}