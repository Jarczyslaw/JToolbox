using System;

namespace JToolbox.Core.Models.DateRanges
{
    public interface IDateRange
    {
        DateTime Begin { get; }

        DateTime End { get; }

        void Set(DateTime begin, DateTime end);
    }
}