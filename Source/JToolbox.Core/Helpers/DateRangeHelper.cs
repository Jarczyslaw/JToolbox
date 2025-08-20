using JToolbox.Core.Extensions;
using JToolbox.Core.Models.DateRanges;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Helpers
{
    public static class DateRangeHelper
    {
        public static (bool Overlap, List<DateRange> OrderedRanges) CheckInternalOverlapping(List<DateRange> ranges)
        {
            if (!ValidateCheckInternalOverlapping(ranges)) { return (false, ranges); }

            List<DateRange> ordered = Order(ranges);

            return (CheckInternalOverlappingOrdered(ordered), ordered);
        }

        public static bool CheckInternalOverlappingOrdered(List<DateRange> orderedRanges)
        {
            if (!ValidateCheckInternalOverlapping(orderedRanges)) { return false; }

            for (int i = 1; i < orderedRanges.Count; i++)
            {
                if (orderedRanges[i - 1].End > orderedRanges[i].Begin)
                {
                    return true;
                }
            }

            return false;
        }

        public static List<DateRange> GetComplementary(List<DateRange> ranges)
        {
            if (!ValidateGetComplementary(ranges)) { return new List<DateRange>(); }

            List<DateRange> orderedRanges = Order(ranges);

            return GetComplementaryOrdered(orderedRanges);
        }

        public static List<DateRange> GetComplementaryOrdered(List<DateRange> orderedRanges)
        {
            if (!ValidateGetComplementary(orderedRanges)) { return new List<DateRange>(); }

            orderedRanges = MergeOrdered(orderedRanges);

            List<DateRange> result = new List<DateRange>();

            for (int i = 0; i < orderedRanges.Count - 1; i++)
            {
                DateRange current = orderedRanges[i];
                DateRange next = orderedRanges[i + 1];

                DateRange complementary = new DateRange(current.End, next.Begin);

                result.Add(complementary);
            }

            return result;
        }

        public static List<DateRange> GetDifference(
            List<DateRange> ranges1,
            List<DateRange> ranges2)
        {
            if (!ValidateGetDifference(ranges1, ranges2)) { return ranges1; }

            List<DateRange> orderedRanges1 = Order(ranges1);
            List<DateRange> orderedRanges2 = Order(ranges2);

            return GetDifferenceOrdered(orderedRanges1, orderedRanges2);
        }

        public static List<DateRange> GetDifferenceOrdered(
            List<DateRange> orderedRanges1,
            List<DateRange> orderedRanges2)
        {
            if (!ValidateGetDifference(orderedRanges1, orderedRanges2)) { return orderedRanges1; }

            orderedRanges1 = MergeOrdered(orderedRanges1);
            orderedRanges2 = MergeOrdered(orderedRanges2);

            int index1 = 0;
            int index2 = 0;

            while (index1 < orderedRanges1.Count && index2 < orderedRanges2.Count)
            {
                DateRange range1 = orderedRanges1[index1];
                DateRange range2 = orderedRanges2[index2];

                List<DateRange> difference = range1.GetDifference(range2);

                if (difference.Count == 1)
                {
                    orderedRanges1[index1] = difference[0];
                }
                else if (difference.Count == 2)
                {
                    orderedRanges1[index1] = difference[0];
                    if (index1 + 1 > orderedRanges1.MaxIndex())
                    {
                        orderedRanges1.Add(difference[1]);
                    }
                    else
                    {
                        orderedRanges1.Insert(index1 + 1, difference[1]);
                    }
                }
                else
                {
                    orderedRanges1.RemoveAt(index1);
                    continue;
                }

                if (range1.End < range2.End) { index1++; }
                else if (range1.End > range2.End) { index2++; }
                else
                {
                    index1++;
                    index2++;
                }
            }

            return orderedRanges1;
        }

        public static TimeSpan GetDuration(IEnumerable<DateRange> ranges) => ranges.Select(x => x.Duration).Sum();

        public static List<DateRange> GetIntersection(
            List<DateRange> ranges1,
            List<DateRange> ranges2)
        {
            if (!ValidateGetIntersection(ranges1, ranges2)) { return new List<DateRange>(); }

            List<DateRange> orderedRanges1 = Order(ranges1);
            List<DateRange> orderedRanges2 = Order(ranges2);

            return GetIntersectionOrdered(orderedRanges1, orderedRanges2);
        }

        public static List<DateRange> GetIntersectionOrdered(
            List<DateRange> orderedRanges1,
            List<DateRange> orderedRanges2)
        {
            if (!ValidateGetIntersection(orderedRanges1, orderedRanges2)) { return new List<DateRange>(); }

            orderedRanges1 = MergeOrdered(orderedRanges1);
            orderedRanges2 = MergeOrdered(orderedRanges2);

            List<DateRange> result = new List<DateRange>();

            int index1 = 0;
            int index2 = 0;

            while (index1 < orderedRanges1.Count && index2 < orderedRanges2.Count)
            {
                DateRange range1 = orderedRanges1[index1];
                DateRange range2 = orderedRanges2[index2];

                DateRange intersection = range1.GetIntersection(range2, includeBoundaries: false);
                if (intersection != null)
                {
                    result.Add(intersection);
                }

                if (range1.End < range2.End) { index1++; }
                else if (range1.End > range2.End) { index2++; }
                else
                {
                    index1++;
                    index2++;
                }
            }

            return MergeOrdered(result);
        }

        public static (bool result, List<DateRange> resultRanges) Include(List<DateRange> ranges, DateTime date)
        {
            List<DateRange> resultRanges = ranges.Where(x => x.Includes(date))
                .ToList();

            return (resultRanges.Count > 0, resultRanges);
        }

        public static bool IsOrdered(List<DateRange> ranges)
        {
            if (ranges.Count < 2) { return true; }

            for (int i = 0; i < ranges.Count - 1; i++)
            {
                DateRange current = ranges[i];
                DateRange next = ranges[i + 1];

                if (current.Begin > next.Begin) { return false; }
            }

            return true;
        }

        public static List<DateRange> Merge(
            List<DateRange> ranges1,
            List<DateRange> ranges2)
        {
            List<DateRange> allRanges = ranges1.Concat(ranges2)
                .ToList();

            return Merge(allRanges);
        }

        public static List<DateRange> Merge(List<DateRange> ranges)
        {
            if (!ValidateMerge(ranges)) { return ranges; }

            List<DateRange> orderedRanges = Order(ranges);

            return MergeOrdered(orderedRanges);
        }

        public static List<DateRange> MergeOrdered(List<DateRange> orderedRanges)
        {
            if (!ValidateMerge(orderedRanges)) { return orderedRanges.ToList(); }

            List<DateRange> result = new List<DateRange>() { orderedRanges[0] };

            for (int i = 1; i < orderedRanges.Count; i++)
            {
                DateRange currentRange = orderedRanges[i];
                DateRange lastRange = result[result.Count - 1];

                DateRange merged = currentRange.Merge(lastRange);
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

        public static List<DateRange> Order(IEnumerable<DateRange> ranges) => ranges.OrderBy(x => x.Begin).ToList();

        public static (List<DateRange> LeftSide, List<DateRange> RightSide) SplitByDate(List<DateRange> ranges, DateTime date)
        {
            List<DateRange> leftSide = new List<DateRange>();
            List<DateRange> rightSide = new List<DateRange>();

            foreach (DateRange range in ranges)
            {
                if (range.Begin < date && range.End <= date)
                {
                    leftSide.Add(range);
                }
                else if (range.Begin >= date && range.End > date)
                {
                    rightSide.Add(range);
                }
                else if (date > range.Begin && date < range.End)
                {
                    leftSide.Add(new DateRange(range.Begin, date));
                    rightSide.Add(new DateRange(date, range.End));
                }
            }

            return (leftSide, rightSide);
        }

        public static List<DateRange> Trim(List<DateRange> ranges, DateTime startDate, DateTime endDate)
        {
            List<DateRange> result = new List<DateRange>();

            DateRange window = new DateRange(startDate, endDate);

            foreach (DateRange range in ranges)
            {
                DateRange intersection = window.GetIntersection(range);
                if (intersection != null)
                {
                    result.Add(intersection);
                }
            }

            return result;
        }

        public static List<DateRange> TrimEnd(List<DateRange> ranges, DateTime date)
        {
            List<DateRange> result = new List<DateRange>();

            foreach (DateRange range in ranges)
            {
                if (range.Begin < date && range.End <= date)
                {
                    result.Add(range);
                }
                else if (range.Begin < date && date < range.End)
                {
                    result.Add(new DateRange(range.Begin, date));
                }
            }

            return result;
        }

        public static List<DateRange> TrimStart(List<DateRange> ranges, DateTime date)
        {
            List<DateRange> result = new List<DateRange>();

            foreach (DateRange range in ranges)
            {
                if (range.Begin >= date && range.End > date)
                {
                    result.Add(range);
                }
                else if (date > range.Begin && date < range.End)
                {
                    result.Add(new DateRange(date, range.End));
                }
            }

            return result;
        }

        private static bool ValidateCheckInternalOverlapping(List<DateRange> ranges) => ranges.Count >= 2;

        private static bool ValidateGetComplementary(List<DateRange> ranges) => ranges.Count >= 2;

        private static bool ValidateGetDifference(
            List<DateRange> ranges1,
            List<DateRange> ranges2) => ranges1.Count >= 1 && ranges2.Count >= 1;

        private static bool ValidateGetIntersection(
            List<DateRange> ranges1,
            List<DateRange> ranges2) => ranges1.Count >= 1 || ranges2.Count >= 1;

        private static bool ValidateMerge(List<DateRange> ranges) => ranges.Count >= 2;
    }
}