using System.Collections.Generic;

namespace JToolbox.Core.Models
{
    public class DateRangeComparer : IEqualityComparer<DateRange>
    {
        public bool Equals(DateRange x, DateRange y)
        {
            return x.Start == y.Start && x.End == y.End;
        }

        public int GetHashCode(DateRange obj)
        {
            return obj.GetHashCode();
        }
    }
}