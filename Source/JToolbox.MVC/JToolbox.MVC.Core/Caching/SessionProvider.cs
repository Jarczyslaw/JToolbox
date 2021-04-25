using System;
using System.Web;
using System.Web.SessionState;

namespace JToolbox.MVC.Core.Caching
{
    public class SessionProvider : ISessionProvider
    {
        public HttpSessionState Session => HttpContext.Current.Session;

        public void Clear()
        {
            Session.Clear();
        }

        public T Get<T>(string key)
        {
            return (T)Session[key];
        }

        public object Get(string key)
        {
            return Session[key];
        }

        public T GetOrSet<T>(string key, Func<T> func, TimeSpan? duration = null)
        {
            if (IsSet(key))
            {
                return Get<T>(key);
            }
            else
            {
                var value = func();
                Set(key, value);
                return value;
            }
        }

        public bool IsSet(string key)
        {
            return Session[key] != null;
        }

        public void Remove(string key)
        {
            Session.Remove(key);
        }

        public void Set(string key, object value, TimeSpan? duration = null)
        {
            Session[key] = value;
        }
    }
}