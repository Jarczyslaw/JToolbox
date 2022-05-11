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
        {
            Start =
                End = date;
        }

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateRange(DateRange dateRange)
        {
            Start = dateRange.Start;
            End = dateRange.End;
        }

        public TimeSpan Duration => End - Start;

        public DateTime End
        {
            get => end;
            set
            {
                if (value < start)
                {
                    throw new ArgumentException("End date must be higher than start date");
                }
                end = value;
            }
        }

        public DateTime Start
        {
            get => start;
            set
            {
                if (value > End)
                {
                    throw new ArgumentException("End date must be higher than start date");
                }
                start = value;
            }
        }

        public static bool operator !=(DateRange range1, DateRange range2) => !(range1 == range2);

        public static bool operator ==(DateRange range1, DateRange range2)
        {
            if ((range1 is null) ^ (range2 is null)) { return false; }

            if (range1 is null && range2 is null) { return false; }

            return range1.Equals(range2);
        }

        public DateRange CommonPart(DateRange dateRange)
        {
            if (!Overlaps(dateRange)) { return null; }

            var start = DateTimeHelper.Max(Start, dateRange.Start);
            var end = DateTimeHelper.Min(End, dateRange.End);
            return new DateRange(start, end);
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

        public bool Overlaps(DateRange dateRange) => dateRange.End >= Start && dateRange.Start <= End;
    }
}