using JToolbox.Core.Exceptions;
using JToolbox.Core.Extensions;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace JToolbox.Core.Utilities
{
    public static class PropertyResolver
    {
        public static object Resolve(object @object, string path)
        {
            if (string.IsNullOrEmpty(path) || @object == null) { throw new ArgumentException(); }

            var properties = path.Split('.');
            var nestedObject = @object;

            foreach (var property in properties)
            {
                nestedObject = GetNestedObject(nestedObject, property);

                if (nestedObject == null && properties.Last() != property)
                {
                    throw new CanNotResolvePathException(path);
                }
            }

            return nestedObject;
        }

        private static CollectionProperty GetCollectionProperty(string propertyName)
        {
            var indexRaw = propertyName.ExtractBetween("[", "]");
            if (string.IsNullOrEmpty(indexRaw) || !int.TryParse(indexRaw, out int index)) { return null; }

            return new CollectionProperty
            {
                Index = index,
                Name = propertyName.Substring(0, propertyName.IndexOf("["))
            };
        }

        private static object GetNestedObject(object @object, string property)
        {
            var type = @object.GetType();

            var collectionProperty = GetCollectionProperty(property);
            if (collectionProperty == null)
            {
                var propertyInfo = type.GetProperty(property);

                return propertyInfo == null
                    ? throw new PropertyNotFoundException(type, property)
                    : propertyInfo.GetValue(@object, null);
            }
            else
            {
                var propertyInfo = type.GetProperty(collectionProperty.Name) ?? throw new PropertyNotFoundException(type, property);

                if (TryGetValueFromList(@object, propertyInfo, collectionProperty, out object result))
                {
                    return result;
                }
                else if (TryGetValueFromDictionary(@object, propertyInfo, collectionProperty, out result))
                {
                    return result;
                }

                throw new NotSupportedException();
            }
        }

        private static bool TryGetValueFromDictionary(
            object @object,
            PropertyInfo propertyInfo,
            CollectionProperty collectionProperty,
            out object result)
        {
            if (typeof(IDictionary).IsAssignableFrom(propertyInfo.PropertyType))
            {
                var dictionary = propertyInfo.GetValue(@object, null) as IDictionary;

                if (dictionary?.Contains(collectionProperty.Index) != true)
                {
                    throw new CanNotAccessCollectionItemException(collectionProperty.Name, collectionProperty.Index);
                }

                result = dictionary[collectionProperty.Index];
                return true;
            }

            result = null;
            return false;
        }

        private static bool TryGetValueFromList(
                    object @object,
            PropertyInfo propertyInfo,
            CollectionProperty collectionProperty,
            out object result)
        {
            if (typeof(IList).IsAssignableFrom(propertyInfo.PropertyType))
            {
                if (!(propertyInfo.GetValue(@object, null) is IList list) || collectionProperty.Index > list.Count - 1)
                {
                    throw new CanNotAccessCollectionItemException(collectionProperty.Name, collectionProperty.Index);
                }

                result = list[collectionProperty.Index];
                return true;
            }

            result = null;
            return false;
        }

        private class CollectionProperty
        {
            public int Index { get; set; }

            public string Name { get; set; }
        }
    }
}