using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace JToolbox.Core.Utilities
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumDescription : Attribute
    {
        private static readonly ConcurrentDictionary<Enum, string> _cachedDescriptions
            = new ConcurrentDictionary<Enum, string>();

        public EnumDescription(string description)
        {
            Description = description;
        }

        public string Description { get; }

        public static string Get(Enum @enum)
            => _cachedDescriptions.GetOrAdd(@enum, x => GetDescriptionFromAttribute(x));

        private static string GetDescriptionFromAttribute(Enum @enum)
        {
            FieldInfo field = @enum.GetType()
                .GetField(@enum.ToString(), BindingFlags.Public | BindingFlags.Static);

            EnumDescription attribute = GetCustomAttribute(field, typeof(EnumDescription)) as EnumDescription;

            return attribute?.Description ?? @enum.ToString();
        }
    }
}