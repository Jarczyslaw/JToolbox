using JToolbox.Core.Abstraction;
using JToolbox.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.DataAccess.Common
{
    public class Merge<T>
        where T : IKey
    {
        public List<T> ToCreate { get; set; }
        public List<T> ToDelete { get; set; }
        public List<T> ToUpdate { get; set; }

        public void MergeLists(List<T> newList, List<T> currentList, IEqualityComparer<T> comparer)
        {
            PrepareItems(newList);

            ToUpdate = newList.Intersect(currentList, comparer)
                .ToList();
            ToCreate = newList.Except(currentList, comparer)
                .ToList();
            ToDelete = currentList.Except(newList, comparer)
                .ToList();
        }

        private void PrepareItems(List<T> newList)
        {
            var id = newList.MinOrDefault(x => x.Id, 0);
            for (int i = 0; i < newList.Count; i++)
            {
                var item = newList[i];
                if (item.Id == 0)
                {
                    --id;
                    item.Id = id;
                }
            }
        }
    }
}