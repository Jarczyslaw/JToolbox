using System;
using System.Web;
using System.Web.Caching;

namespace JToolbox.MVC.Core.Caching
{
    public class HttpContextCacheProvider : IHttpContextCacheProvider
    {
        public Cache Cache => HttpContext.Current.Cache;

        public void Clear()
        {
            var enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Cache.Remove((string)enumerator.Key);
            }
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
            return Cache.Get(key) != null;
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void Set(string key, object value, TimeSpan? duration = null)
        {
            if (duration.HasValue)
            {
                Cache.Insert(key, value, null, DateTime.Now + duration.Value, Cache.NoSlidingExpiration);
            }
            else
            {
                Cache.Insert(key, value);
            }
        }
    }
}