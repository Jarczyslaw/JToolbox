using System;
using System.Collections.Generic;

namespace JToolbox.Core.EqualityComparers
{
    public class GenericEqualityComparer<TType, TPropertyType> : IEqualityComparer<TType>
        where TType : class
    {
        private readonly Func<TType, TPropertyType> _propertySelector;

        public GenericEqualityComparer(Func<TType, TPropertyType> propertySelector)
        {
            _propertySelector = propertySelector;
        }

        public bool Equals(TType x, TType y)
        {
            if (ReferenceEquals(x, y)) { return true; }
            else if (x == null || y == null) { return false; }

            return EqualityComparer<TPropertyType>.Default.Equals(_propertySelector(x), _propertySelector(y));
        }

        public int GetHashCode(TType obj)
        {
            if (obj == null) { return 0; }

            return _propertySelector(obj)?.GetHashCode() ?? 0;
        }
    }

    public class GenericEqualityComparer<TType, TPropertyType1, TPropertyType2> : IEqualityComparer<TType>
        where TType : class
    {
        private readonly Func<TType, TPropertyType1> _property1Selector;
        private readonly Func<TType, TPropertyType2> _property2Selector;

        public GenericEqualityComparer(
            Func<TType, TPropertyType1> property1Selector,
            Func<TType, TPropertyType2> property2Selector)
        {
            _property1Selector = property1Selector;
            _property2Selector = property2Selector;
        }

        public bool Equals(TType x, TType y)
        {
            if (ReferenceEquals(x, y)) { return true; }
            else if (x == null || y == null) { return false; }

            return EqualityComparer<TPropertyType1>.Default.Equals(_property1Selector(x), _property1Selector(y))
                && EqualityComparer<TPropertyType2>.Default.Equals(_property2Selector(x), _property2Selector(y));
        }

        public int GetHashCode(TType obj)
        {
            if (obj == null) { return 0; }

            var hashCode = new HashCode();
            hashCode.Add(_property1Selector(obj));
            hashCode.Add(_property2Selector(obj));

            return hashCode.ToHashCode();
        }
    }

    public class GenericEqualityComparer<TType, TPropertyType1, TPropertyType2, TPropertyType3> : IEqualityComparer<TType>
        where TType : class
    {
        private readonly Func<TType, TPropertyType1> _property1Selector;
        private readonly Func<TType, TPropertyType2> _property2Selector;
        private readonly Func<TType, TPropertyType3> _property3Selector;

        public GenericEqualityComparer(
            Func<TType, TPropertyType1> property1Selector,
            Func<TType, TPropertyType2> property2Selector,
            Func<TType, TPropertyType3> property3Selector)
        {
            _property1Selector = property1Selector;
            _property2Selector = property2Selector;
            _property3Selector = property3Selector;
        }

        public bool Equals(TType x, TType y)
        {
            if (ReferenceEquals(x, y)) { return true; }
            else if (x == null || y == null) { return false; }

            return EqualityComparer<TPropertyType1>.Default.Equals(_property1Selector(x), _property1Selector(y))
                && EqualityComparer<TPropertyType2>.Default.Equals(_property2Selector(x), _property2Selector(y))
                && EqualityComparer<TPropertyType3>.Default.Equals(_property3Selector(x), _property3Selector(y));
        }

        public int GetHashCode(TType obj)
        {
            if (obj == null) { return 0; }

            var hashCode = new HashCode();
            hashCode.Add(_property1Selector(obj));
            hashCode.Add(_property2Selector(obj));
            hashCode.Add(_property3Selector(obj));

            return hashCode.ToHashCode();
        }
    }
}