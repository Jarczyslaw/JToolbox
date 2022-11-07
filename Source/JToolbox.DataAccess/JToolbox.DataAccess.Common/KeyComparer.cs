using JToolbox.Core.Abstraction;
using System.Collections.Generic;

namespace JToolbox.DataAccess.Common
{
    public class KeyComparer<T> : IEqualityComparer<T>
        where T : IKey
    {
        public bool Equals(T x, T y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(T obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}