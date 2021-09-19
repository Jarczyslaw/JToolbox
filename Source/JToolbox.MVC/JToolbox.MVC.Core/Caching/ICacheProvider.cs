using System;

namespace JToolbox.MVC.Core.Caching
{
    public interface ICacheProvider
    {
        void Clear();

        object Get(string key);

        T Get<T>(string key);

        T GetOrSet<T>(string key, Func<T> func, TimeSpan? duration = null);

        bool IsSet(string key);

        void Remove(string key);

        void Set(string key, object value, TimeSpan? duration = null);
    }
}