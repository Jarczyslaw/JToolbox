using System;

namespace JToolbox.Core.Exceptions
{
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException(Type type, string propertyName)
            : base($"Property {propertyName} not found in type {type.Name}")
        {
        }
    }
}