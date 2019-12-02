using System.Linq;

namespace JToolbox.Core.Extensions
{
    public static class MiscExtensions
    {
        public static bool In<T>(this T val, params T[] values) where T : struct
        {
            return values.Contains(val);
        }
    }
}