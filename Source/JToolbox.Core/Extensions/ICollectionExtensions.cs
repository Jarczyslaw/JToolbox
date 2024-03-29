﻿using System;
using System.Collections.Generic;

namespace JToolbox.Core.Extensions
{
    public static class ICollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> @this, params T[] values)
        {
            foreach (T value in values)
            {
                @this.Add(value);
            }
        }

        public static void AddRange<T>(this ICollection<T> @this, IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                @this.Add(value);
            }
        }

        public static bool ContainsAll<T>(this ICollection<T> @this, params T[] values)
        {
            foreach (T value in values)
            {
                if (!@this.Contains(value))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ContainsAny<T>(this ICollection<T> @this, params T[] values)
        {
            foreach (T value in values)
            {
                if (@this.Contains(value))
                {
                    return true;
                }
            }
            return false;
        }

        public static void ForEach<T>(this ICollection<T> @this, Action<T> action)
        {
            foreach (var item in @this)
            {
                action(item);
            }
        }

        public static bool IsEmpty<T>(this ICollection<T> @this)
        {
            return @this.Count == 0;
        }

        public static void RemoveRange<T>(this ICollection<T> @this, params T[] values)
        {
            foreach (T value in values)
            {
                @this.Remove(value);
            }
        }
    }
}