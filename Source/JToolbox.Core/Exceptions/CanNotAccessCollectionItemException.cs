using System;

namespace JToolbox.Core.Exceptions
{
    public class CanNotResolvePathException : Exception
    {
        public CanNotResolvePathException(string path)
            : base($"{path} can not be fully resolved")
        {
        }
    }
}