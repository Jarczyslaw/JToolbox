using JToolbox.Core.Extensions;
using JToolbox.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Helpers
{
    public static class DateRangeHelper
    {
        public static (bool overlap, List<DateRange> orderedRanges) CheckInternalOverlapping(List<DateRange> ranges)
        {
            var ordered = ranges.OrderBy(x => x.Start)
                .ToList();

            if (ordered.Count > 1)
            {
                for (int i = 1; i < ordered.Count; i++)
                {
                    if (ordered[i - 1].End > ordered[i].Start)
                    {
                        return (true, ordered);
                    }
                }
            }
            return (false, ordered);
        }

        public static TimeSpan GetDuration(IEnumerable<DateRange> ranges) => ranges.Select(x => x.Duration).Sum();

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

        public static List<DateRange> Merge(List<DateRange> ranges, bool includeBoundaries)
        {
            if (ranges == null || ranges.Count < 2) { return ranges; }

            var orderedRanges = ranges.OrderBy(x => x.Start)
                .ToList();

            var result = new List<DateRange>() { orderedRanges[0] };

            for (var i = 1; i < orderedRanges.Count; i++)
            {
                var currentRange = orderedRanges[i];
                var lastRange = result[result.Count - 1];

                var merged = currentRange.Merge(lastRange, includeBoundaries);
                if (merged == null)
                {
                    result.Add(currentRange);
                }
                else
                {
                    result[result.Count - 1] = merged;
                }
            }

            return result;
        }

        private static List<DateRange> OrderByStartAndCheckOverlapping(List<DateRange> ranges)
        {
            (bool overlap, List<DateRange> orderedRanges) = CheckInternalOverlapping(ranges);
            if (overlap)
            {
                throw new ArgumentException("Input ranges overlap internally");
            }

            return orderedRanges;
        }
    }
}