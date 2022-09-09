using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        public static decimal AverageOrDefault<T1>(this IEnumerable<T1> enumerable, Func<T1, decimal> selector, decimal defaultValue = default)
        {
            return enumerable.Any() ? enumerable.Average(selector) : defaultValue;
        }

        public static IEnumerable<List<T>> BatchesOf<T>(this IEnumerable<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList());
        }

        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (var item in @this)
            {
                action(item);
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> @this)
        {
            return @this?.Any() != true;
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> @this)
        {
            return !@this.IsEmpty();
        }

        public static T2 MaxOrDefault<T1, T2>(this IEnumerable<T1> enumerable, Func<T1, T2> selector, T2 defaultValue = default)
        {
            return enumerable.Any() ? enumerable.Max(selector) : defaultValue;
        }

        public static T2 MinOrDefault<T1, T2>(this IEnumerable<T1> enumerable, Func<T1, T2> selector, T2 defaultValue = default)
        {
            return enumerable.Any() ? enumerable.Min(selector) : defaultValue;
        }

        public static bool None<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return !source.Any(predicate);
        }

        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> collection)
            => collection.OrderBy(x => x);

        public static IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> collection)
            => collection.OrderByDescending(x => x);

        public static bool ScrambledEquals<T>(this IEnumerable<T> list1, IEnumerable<T> list2, IEqualityComparer<T> comparer = null)
        {
            var cnt = comparer == null
                ? new Dictionary<T, int>() : new Dictionary<T, int>(comparer);

            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }

            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }

        public static List<T> SearchRecursively<T>(this IEnumerable<T> @this, Func<T, IEnumerable<T>> childrenSelector, Predicate<T> predicate, List<T> result = null)
        {
            result = result ?? new List<T>();

            if (@this != null)
            {
                foreach (var item in @this)
                {
                    if (predicate(item))
                    {
                        result.Add(item);
                    }

                    var children = childrenSelector(item);
                    children.SearchRecursively(childrenSelector, predicate, result);
                }
            }
            return result;
        }

        public static TimeSpan Sum(this IEnumerable<TimeSpan> @this)
        {
            return @this.Aggregate(TimeSpan.Zero, (TimeSpan current, TimeSpan item) => current + item);
        }
    }
}