using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace JToolbox.Core.EqualityComparers
{
    public class ObjectReferenceEqualityComparer<T> : IEqualityComparer<T>
        where T : class
    {
        private static IEqualityComparer<T> _defaultComparer;

        public static IEqualityComparer<T> Default => _defaultComparer ?? (_defaultComparer = new ObjectReferenceEqualityComparer<T>());

        public bool Equals(T x, T y) => ReferenceEquals(x, y);

        public int GetHashCode(T obj) => RuntimeHelpers.GetHashCode(obj);
    }
}