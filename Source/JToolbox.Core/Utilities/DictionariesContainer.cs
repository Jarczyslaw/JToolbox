using JToolbox.Core.Abstraction;
using JToolbox.Core.Exceptions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Utilities
{
    public class DictionariesContainer
    {
        public bool ContainsAnyData => Dictionaries.Values.Any(x => (x as IDictionary)?.Count > 0);

        protected ConcurrentDictionary<Type, object> Dictionaries { get; set; } = new ConcurrentDictionary<Type, object>();

        public static DictionariesContainer Create<T>(IEnumerable<T> collection)
            where T : class, IKey
        {
            DictionariesContainer container = new DictionariesContainer();
            container.Submit(collection);
            return container;
        }

        public void Clear<T>()
            where T : class, IKey
        {
            Dictionary<int, T> dictionary = GetDictionary<T>();
            dictionary.Clear();
        }

        public void Clear() => Dictionaries.Clear();

        public Dictionary<int, T> GetDictionary<T>()
            where T : class, IKey
        {
            Type type = typeof(T);

            if (!Dictionaries.TryGetValue(type, out object value))
            {
                value = new Dictionary<int, T>();
                Dictionaries[type] = value;
            }

            if (value is Dictionary<int, T> dictionary) { return dictionary; }

            throw new InvalidDictionaryType(type);
        }

        public Dictionary<int, T> GetDictionarySnapshot<T>()
            where T : class, IKey
        {
            Dictionary<int, T> dictionary = GetDictionary<T>();
            return new Dictionary<int, T>(dictionary);
        }

        public void Submit<T>(T item)
            where T : class, IKey
        {
            Dictionary<int, T> dictionary = GetDictionary<T>();
            dictionary[item.Id] = item;
        }

        public void Submit<T>(
            IEnumerable<T> items,
            Func<T, bool> removeCondition = null,
            bool clearCurrentData = false)
            where T : class, IKey
        {
            if (!items.Any()) { return; }

            Dictionary<int, T> dictionary = GetDictionary<T>();

            if (clearCurrentData)
            {
                dictionary.Clear();
            }

            if (removeCondition == null) { removeCondition = (T _) => false; }

            foreach (T item in items)
            {
                if (removeCondition(item))
                {
                    dictionary.Remove(item.Id);
                }
                else
                {
                    dictionary[item.Id] = item;
                }
            }
        }

        public void Submit<T>(
            DictionariesContainer other,
            Func<T, bool> removeCondition = null,
            bool clearCurrentData = false)
            where T : class, IKey
        {
            Dictionary<int, T> otherDictionary = other.GetDictionary<T>();
            Submit(otherDictionary.Values, removeCondition, clearCurrentData);
        }
    }
}