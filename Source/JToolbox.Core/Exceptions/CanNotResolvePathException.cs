using System;

namespace JToolbox.Core.Exceptions
{
    public class CanNotAccessCollectionItemException : Exception
    {
        public CanNotAccessCollectionItemException(string propertyName, int index)
            : base($"Can not access item with {index} in collection indicated by property {propertyName}")
        {
        }
    }
}