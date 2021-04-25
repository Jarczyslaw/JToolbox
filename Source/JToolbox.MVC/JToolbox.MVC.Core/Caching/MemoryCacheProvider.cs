using System;
using System.Runtime.Caching;

namespace JToolbox.MVC.Core.Caching
{
    public class MemoryCacheProvider : IMemoryCacheProvider
    {
        public MemoryCache Cache => MemoryCache.Default;

        public void Clear()
        {
            Cache.Dispose();
        }

        public object Get(string key)
        {
            return Cache.Get(key);
        }

        public T Get<T>(string key)
        {
            return (T)Get(key);
        }

        public T GetOrSet<T>(string key, Func<T> func, TimeSpan? duration = null)
        {
            if (IsSet(key))
            {
                return Get<T>(key);
            }
            else
            {
                T newValue = func();
                Set(key, newValue, duration);
                return newValue;
            }
        }

        public bool IsSet(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void Set(string key, object value, TimeSpan? duration = null)
        {
            DateTimeOffset expiration = DateTime.Now;
            if (duration.HasValue)
            {
                expiration += duration.Value;
            }
            Cache.Set(key, value, expiration);
        }
    }
}