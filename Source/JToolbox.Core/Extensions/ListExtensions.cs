using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Extensions
{
    public static class ListExtensions
    {
        public static List<T> AsNotEmpty<T>(this List<T> @this)
        {
            return @this ?? new List<T>();
        }

        public static List<List<T>> ChunkBy<T>(this List<T> list, int chunkSize)
        {
            var result = new List<List<T>>();
            for (int i = 0; ; i++)
            {
                var index = i * chunkSize;
                if (index >= list.Count)
                {
                    break;
                }
                result.Add(list.GetRange(index, Math.Min(chunkSize, list.Count - index)));
            }
            return result;
        }

        public static List<List<T>> ChunkInto<T>(this List<T> list, int chunksCount)
        {
            var result = new List<List<T>>();
            if (chunksCount > list.Count)
            {
                chunksCount = list.Count;
            }
            var chunkSize = (float)list.Count / chunksCount;
            if (chunkSize < 1f)
            {
                chunkSize = 1f;
            }

            for (int i = 0; i < chunksCount; i++)
            {
                var index = (int)Math.Round(i * chunkSize);
                var itemsToAdd = (int)Math.Round((i + 1) * chunkSize) - index;
                result.Add(list.GetRange(index, itemsToAdd));
            }
            return result;
        }

        public static void IncludeMany<T1, T2, TKey>(
            this List<T1> collection,
            List<T2> toInclude,
            Func<T1, TKey> collectionKeySelector,
            Func<T2, TKey> toIncludeKeySelector,
            Action<T1, List<T2>> includeAction)
        {
            var dict = toInclude.GroupBy(toIncludeKeySelector).ToDictionary(a => a.Key, a => a.ToList());
            foreach (var entity in collection)
            {
                var list = new List<T2>();
                var key = collectionKeySelector(entity);
                if (dict.TryGetValue(key, out List<T2> value))
                {
                    list = value;
                }
                includeAction(entity, list);
            }
        }

        public static void IncludeOne<T1, T2, TKey>(
            this List<T1> collection,
            List<T2> toInclude,
            Func<T1, TKey> collectionKeySelector,
            Func<T2, TKey> toIncludeKeySelector,
            Action<T1, T2> includeAction)
        {
            IncludeMany(collection, toInclude,
                collectionKeySelector, toIncludeKeySelector, (e1, e2) => includeAction(e1, e2.SingleOrDefault()));
        }

        public static List<T> Peek<T>(this List<T> list, int count)
        {
            var result = list.Take(count)
                .ToList();
            if (result.Count > 0)
            {
                list.RemoveRange(0, result.Count());
            }
            return result;
        }

        public static void Replace<T>(this List<T> list, T item, Predicate<T> predicate)
        {
            var index = list.FindIndex(predicate);
            if (index != -1)
            {
                list[index] = item;
            }
        }

        public static void SortDescending<T>(this List<T> @this)
        {
            @this.Sort();
            @this.Reverse();
        }
    }
}