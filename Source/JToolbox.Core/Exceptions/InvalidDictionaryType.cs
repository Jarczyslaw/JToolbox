using System;

namespace JToolbox.Core.Exceptions
{
    public class InvalidDictionaryType : Exception
    {
        public InvalidDictionaryType(Type type)
            : base($"Inconsistency found between data type ({type.Name}) and dictionary type in container")
        {
        }
    }
}