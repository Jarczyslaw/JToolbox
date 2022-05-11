using System;

namespace JToolbox.Core.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime Max(DateTime date1, DateTime date2)
        {
            if (date1 >= date2) { return date1; }
            return date2;
        }

        public static DateTime Min(DateTime date1, DateTime date2)
        {
            if (date1 <= date2) { return date1; }
            return date2;
        }
    }
}