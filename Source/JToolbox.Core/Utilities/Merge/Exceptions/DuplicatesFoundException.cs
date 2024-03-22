using System;

namespace JToolbox.Core.Utilities.Merge.Exceptions
{
    public class DuplicatesFoundException : Exception
    {
        public DuplicatesFoundException(string collectionName)
            : base($"Collection {collectionName} contains duplicates")
        {
        }
    }
}