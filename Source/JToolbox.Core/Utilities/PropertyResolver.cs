using JToolbox.Core.Exceptions;
using JToolbox.Core.Extensions;
using System;
using System.Collections;
using System.Linq;

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

                if (propertyInfo == null) { throw new PropertyNotFoundException(type, property); }

                return propertyInfo.GetValue(@object, null);
            }
            else
            {
                var propertyInfo = type.GetProperty(collectionProperty.Name);

                if (propertyInfo == null) { throw new PropertyNotFoundException(type, property); }

                if (typeof(IList).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    var list = propertyInfo.GetValue(@object, null) as IList;

                    if (list == null || collectionProperty.Index > list.Count - 1)
                    {
                        throw new CanNotAccessCollectionItemException(collectionProperty.Name, collectionProperty.Index);
                    }

                    return list[collectionProperty.Index];
                }
                else if (typeof(IDictionary).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    var dictionary = propertyInfo.GetValue(@object, null) as IDictionary;

                    if (dictionary?.Contains(collectionProperty.Index) != true)
                    {
                        throw new CanNotAccessCollectionItemException(collectionProperty.Name, collectionProperty.Index);
                    }

                    return dictionary[collectionProperty.Index];
                }

                throw new NotSupportedException();
            }
        }

        private class CollectionProperty
        {
            public int Index { get; set; }

            public string Name { get; set; }
        }
    }
}