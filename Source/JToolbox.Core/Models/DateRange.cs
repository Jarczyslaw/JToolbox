using JToolbox.Core.Helpers;
using System;

namespace JToolbox.Core.Models
{
    public class DateRange : IEquatable<DateRange>
    {
        private DateTime end;
        private DateTime start;

        public DateRange()
        {
        }

        public DateRange(DateTime date)
            : this(date, date)
        {
        }

        public DateRange(DateTime start, DateTime end)
        {
            this.start = start;
            this.end = end;
            CheckDatesChronology();
        }

        public DateRange(DateRange dateRange)
        {
            start = dateRange.Start;
            end = dateRange.End;
        }

        public TimeSpan Duration => End - Start;

        public DateTime End
        {
            get => end;
            set
            {
                end = value;
                CheckDatesChronology();
            }
        }

        public DateTime Start
        {
            get => start;
            set
            {
                start = value;
                CheckDatesChronology();
            }
        }

        public static bool operator !=(DateRange range1, DateRange range2) => !(range1 == range2);

        public static bool operator ==(DateRange range1, DateRange range2)
        {
            if ((range1 is null) ^ (range2 is null)) { return false; }

            if (range1 is null && range2 is null) { return true; }

            return range1.Equals(range2);
        }

        public bool Equals(DateRange other)
        {
            if (other == null) { return false; }

            if (ReferenceEquals(this, other)) { return true; }

            return other.Start == Start && other.End == End;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DateRange other))
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode() => (Start, End).GetHashCode();

        public bool Includes(DateTime dateTime) => dateTime >= Start && dateTime <= End;

        public DateRange Intersection(DateRange dateRange, bool includeBoundaries)
        {
            if (!Overlaps(dateRange, includeBoundaries)) { return null; }

            var start = DateTimeHelper.Max(Start, dateRange.Start);
            var end = DateTimeHelper.Min(End, dateRange.End);

            return new DateRange(start, end);
        }

        public bool Overlaps(DateRange dateRange, bool includeBoundaries)
        {
            if (includeBoundaries)
            {
                return dateRange.End >= Start && dateRange.Start <= End;
            }
            else
            {
                return dateRange.End > Start && dateRange.Start < End;
            }
        }

        private void CheckDatesChronology()
        {
            if (start > end)
            {
                throw new Exception("End date isolder than start date");
            }
        }
    }
}