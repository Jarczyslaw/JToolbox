using System.Collections.Generic;

namespace JToolbox.Core.Extensions
{
    public static class IListExtensions
    {
        public static void ShiftLeft<T>(this IList<T> list, int index)
        {
            if (list.Count < 2)
                return;

            if (index < 1)
                return;

            var temp = list[index - 1];
            list.RemoveAt(index - 1);
            list.Insert(index, temp);
        }

        public static void ShiftRight<T>(this IList<T> list, int index)
        {
            if (list.Count < 2)
                return;

            if (index > list.Count - 2)
                return;

            var temp = list[index + 1];
            list.RemoveAt(index + 1);
            list.Insert(index, temp);
        }
    }
}