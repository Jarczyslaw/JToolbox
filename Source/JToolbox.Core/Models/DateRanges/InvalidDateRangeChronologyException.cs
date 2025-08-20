using System;

namespace JToolbox.Core.Models.DateRanges
{
    public class InvalidDateRangeChronologyException : Exception
    {
        public InvalidDateRangeChronologyException(DateTime begin, DateTime end)
            : base($"End date {end} is equal or older than begin date {begin}")
        {
        }
    }
}