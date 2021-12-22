using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Extensions
{
    public static class GenericExtensions
    {
        public static List<T> AsList<T>(this T @this)
        {
            return new List<T> { @this };
        }

        public static bool In<T>(this T source, params T[] list)
        {
            return list.Contains(source);
        }

        public static bool NotIn<T>(this T source, params T[] list)
        {
            return !list.Contains(source);
        }
    }
}