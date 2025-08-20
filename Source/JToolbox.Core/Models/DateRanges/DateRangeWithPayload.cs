using System;

namespace JToolbox.Core.Models.DateRanges
{
    public class DateRangeWithPayload<T> : DateRange
    {
        public DateRangeWithPayload(T payload, DateTime begin, DateTime end)
            : base(begin, end)
        {
            Payload = payload;
        }

        public DateRangeWithPayload(T payload, DateRange dateRange)
            : base(dateRange)
        {
            Payload = payload;
        }

        public T Payload { get; }
    }
}