using System;
using System.Collections.Generic;

namespace JToolbox.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> @this, Action<KeyValuePair<TKey, TValue>> action)
        {
            foreach (KeyValuePair<TKey, TValue> item in @this)
            {
                action(item);
            }
        }
    }
}