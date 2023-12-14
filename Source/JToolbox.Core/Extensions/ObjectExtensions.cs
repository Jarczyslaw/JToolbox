using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace JToolbox.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static string AllPropertiesToString(this object @this)
        {
            return @this.PropertiesToString(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static Dictionary<TKey, TValue> AsDictionary<TKey, TValue>(this TValue @this, Func<TKey> keySelector, bool allowNull = false)
        {
            if (allowNull && @this == null) { return null; }

            return new Dictionary<TKey, TValue> { [keySelector()] = @this };
        }

        public static List<T> AsList<T>(this T @this, bool allowNull = false)
        {
            if (allowNull && @this == null) { return null; }

            return new List<T> { @this };
        }

        public static T DeepClone<T>(this object obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }

        public static object GetPropValue(this object @this, string propName)
        {
            return @this?.GetType().GetProperty(propName).GetValue(@this, null);
        }

        public static T GetPropValue<T>(this object @this, string propName)
        {
            return (T)@this?.GetType().GetProperty(propName).GetValue(@this, null);
        }

        public static IEnumerable<PropertyInfo> GetPublicProperties(this object @this)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            return @this.GetType().GetProperties(flags);
        }

        public static string PropertiesToString(this object @this, BindingFlags flags)
        {
            var result = new StringBuilder();
            var props = @this.GetType().GetProperties(flags);
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                var propVal = prop.GetValue(@this, null);
                var propStr = $"{prop.Name} = {(propVal != null ? propVal.ToString() : string.Empty)}";
                if (i != props.Length - 1)
                {
                    result.AppendLine(propStr);
                }
                else
                {
                    result.Append(propStr);
                }
            }
            return result.ToString();
        }

        public static string PublicPropertiesToString(this object @this)
        {
            return @this.PropertiesToString(BindingFlags.Public | BindingFlags.Instance);
        }

        public static ExpandoObject ToExpandoObject(this object @this)
        {
            var expando = new ExpandoObject();
            foreach (PropertyInfo property in @this.GetPublicProperties())
            {
                expando.Set(property.Name, property.GetValue(@this));
            }

            return expando;
        }
    }
}