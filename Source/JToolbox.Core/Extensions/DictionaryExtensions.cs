using System;
using System.Collections.Generic;

namespace JToolbox.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> @this, IEnumerable<TValue> source, Func<TValue, TKey> keySelector, bool update = false)
        {
            source.ForEach(x =>
            {
                var key = keySelector(x);
                if (@this.TryGetValue(key, out TValue value))
                {
                    if (update)
                    {
                        @this[key] = value;
                    }
                }
                else
                {
                    @this.Add(key, x);
                }
            });
        }

        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> @this, Action<KeyValuePair<TKey, TValue>> action)
        {
            foreach (KeyValuePair<TKey, TValue> item in @this)
            {
                action(item);
            }
        }
    }
}