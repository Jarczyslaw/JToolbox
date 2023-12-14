using System;
using System.Collections.Generic;
using System.Dynamic;

namespace JToolbox.Core.Extensions
{
    public static class ExpandoObjectExtensions
    {
        public static IDictionary<string, object> AsDictionary(this ExpandoObject @this) => @this;

        public static T Get<T>(this ExpandoObject @this, string propertyName)
            where T : class
            => Get(@this, propertyName) as T;

        public static object Get(this ExpandoObject @this, string propertyName)
        {
            var expandoDict = AsDictionary(@this);
            if (expandoDict.TryGetValue(propertyName, out object value))
            {
                return value;
            }
            throw new ArgumentException($"Property {propertyName} not exists in expando object");
        }

        public static void Set(this ExpandoObject @this, string propertyName, object propertyValue = null)
        {
            var expandoDict = AsDictionary(@this);
            if (expandoDict.ContainsKey(propertyName))
            {
                expandoDict[propertyName] = propertyValue;
            }
            else
            {
                expandoDict.Add(propertyName, propertyValue);
            }
        }
    }
}