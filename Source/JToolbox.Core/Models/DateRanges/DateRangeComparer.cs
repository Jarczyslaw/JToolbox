using System.Collections.Generic;

namespace JToolbox.Core.Models.DateRanges
{
    public class DateRangeComparer : IEqualityComparer<DateRange>
    {
        public bool Equals(DateRange x, DateRange y)
        {
            return x.Begin == y.Begin && x.End == y.End;
        }

        public int GetHashCode(DateRange obj)
        {
            return obj.GetHashCode();
        }
    }
}