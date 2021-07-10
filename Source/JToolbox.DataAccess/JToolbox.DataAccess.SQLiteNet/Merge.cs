using JToolbox.DataAccess.SQLiteNet.Entities;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.DataAccess.SQLiteNet
{
    public class Merge<T>
        where T : BaseEntity
    {
        public List<T> ToCreate { get; set; }
        public List<T> ToUpdate { get; set; }
        public List<T> ToDelete { get; set; }

        public void MergeLists(List<T> newList, List<T> currentList, IEqualityComparer<T> comparer)
        {
            ToUpdate = newList.Intersect(currentList, comparer)
                .ToList();
            ToCreate = newList.Where(x => x.Id <= 0)
                .ToList();
            ToDelete = currentList.Except(newList, comparer)
                .ToList();
        }
    }
}