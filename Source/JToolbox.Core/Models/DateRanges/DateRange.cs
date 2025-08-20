using JToolbox.Core.Helpers;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Models.DateRanges
{
    public class DateRange : IEquatable<DateRange>, IDateRange
    {
        private DateTime _begin;
        private DateTime _end;

        public DateRange(DateTime begin, DateTime end)
        {
            Set(begin, end);
        }

        public DateRange(DateRange dateRange)
        {
            _begin = dateRange.Begin;
            _end = dateRange.End;
        }

        public DateTime Begin => _begin;

        public TimeSpan Duration => End - Begin;

        public DateTime End => _end;

        public static DateTime Max(DateTime dt1, DateTime dt2) => dt1 > dt2 ? dt1 : dt2;

        public static DateTime Min(DateTime dt1, DateTime dt2) => dt1 < dt2 ? dt1 : dt2;

        public static bool operator !=(DateRange range1, DateRange range2) => !(range1 == range2);

        public static bool operator ==(DateRange range1, DateRange range2)
        {
            if (range1 is null ^ range2 is null) { return false; }

            if (range1 is null && range2 is null) { return true; }

            return range1.Equals(range2);
        }

        public bool Equals(DateRange other)
        {
            if (other == null) { return false; }

            if (ReferenceEquals(this, other)) { return true; }

            return other.Begin == Begin && other.End == End;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DateRange other))
            {
                return false;
            }

            return Equals(other);
        }

        public List<DateRange> GetDifference(DateRange dateRange)
        {
            DateRange intersection = GetIntersection(dateRange, false);

            if (intersection == null) { return new List<DateRange> { this }; }

            List<DateRange> result = new List<DateRange>();

            if (intersection.Begin > Begin)
            {
                result.Add(new DateRange(Begin, intersection.Begin));
            }

            if (End > intersection.End)
            {
                result.Add(new DateRange(intersection.End, End));
            }

            return result;
        }

        public override int GetHashCode() => (Begin, End).GetHashCode();

        public DateRange GetIntersection(DateRange dateRange, bool includeBoundaries = false)
        {
            if (Overlaps(dateRange, includeBoundaries))
            {
                DateTime begin = DateTimeHelper.Max(Begin, dateRange.Begin);
                DateTime end = DateTimeHelper.Min(End, dateRange.End);

                return new DateRange(begin, end);
            }

            return null;
        }

        public bool Includes(DateTime dateTime) => dateTime >= Begin && dateTime <= End;

        public DateRange Merge(DateRange dateRange)
        {
            if (Overlaps(dateRange, includeBoundaries: true))
            {
                DateTime begin = DateTimeHelper.Min(Begin, dateRange.Begin);
                DateTime end = DateTimeHelper.Max(End, dateRange.End);

                return new DateRange(begin, end);
            }

            return null;
        }

        public bool Overlaps(DateRange dateRange, bool includeBoundaries = false)
        {
            return includeBoundaries
                ? dateRange.End >= Begin && dateRange.Begin <= End
                : dateRange.End > Begin && dateRange.Begin < End;
        }

        public void Set(DateTime begin, DateTime end)
        {
            _begin = begin;
            _end = end;

            CheckDatesChronology();
        }

        private void CheckDatesChronology()
        {
            if (_begin >= _end)
            {
                throw new InvalidDateRangeChronologyException(_begin, _end);
            }
        }
    }
}