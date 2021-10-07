using System.Collections.Generic;
using System.Linq;

namespace JToolbox.DataAccess.Common
{
    public class Merge<T>
    {
        public List<T> ToCreate { get; set; }
        public List<T> ToDelete { get; set; }
        public List<T> ToUpdate { get; set; }

        public void MergeLists(List<T> newList, List<T> currentList, IEqualityComparer<T> comparer)
        {
            ToUpdate = newList.Intersect(currentList, comparer)
                .ToList();
            ToCreate = newList.Except(currentList, comparer)
                .ToList();
            ToDelete = currentList.Except(newList, comparer)
                .ToList();
        }
    }
}