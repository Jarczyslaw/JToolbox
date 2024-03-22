using JToolbox.Core.Abstraction;

namespace JToolbox.Core.EqualityComparers
{
    public class KeyComparer : GenericEqualityComparer<IKey, int>
    {
        public KeyComparer() : base(x => x.Id)
        {
        }
    }
}