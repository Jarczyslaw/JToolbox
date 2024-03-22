using JToolbox.Core.Utilities.Merge.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Utilities.Merge
{
    public class MergeCollections<T>
    {
        public List<T> ToCreate { get; private set; } = new List<T>();

        public List<T> ToDelete { get; private set; } = new List<T>();

        public List<MergeToUpdateEntry<T>> ToUpdate { get; private set; } = new List<MergeToUpdateEntry<T>>();

        public void Merge(
            IEnumerable<T> oldCollection,
            IEnumerable<T> newCollection,
            IEqualityComparer<T> comparer = null)
        {
            oldCollection = oldCollection ?? new List<T>();
            newCollection = newCollection ?? new List<T>();

            comparer = comparer ?? EqualityComparer<T>.Default;

            if (CheckDuplicates(oldCollection, comparer)) { throw new DuplicatesFoundException(nameof(oldCollection)); }

            if (CheckDuplicates(newCollection, comparer)) { throw new DuplicatesFoundException(nameof(newCollection)); }

            ToUpdate = GetItemsToUpdate(oldCollection, newCollection, comparer);

            ToCreate = newCollection.Except(oldCollection, comparer)
                .ToList();

            ToDelete = oldCollection.Except(newCollection, comparer)
                .ToList();
        }

        private bool CheckDuplicates(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            var withoutDuplicates = new HashSet<T>(collection, comparer);
            return collection.Count() != withoutDuplicates.Count;
        }

        private List<MergeToUpdateEntry<T>> GetItemsToUpdate(
            IEnumerable<T> oldCollection,
            IEnumerable<T> newCollection,
            IEqualityComparer<T> comparer)
        {
            var oldCollectionLookup = new HashSet<T>(oldCollection);

            Dictionary<T, List<T>> grouped = oldCollection.Concat(newCollection)
                .GroupBy(x => x, comparer)
                .ToDictionary(x => x.Key, x => x.ToList());

            if (grouped.Values.Any(x => x.Count > 2)) { throw new CanNotDetermineToUpdateItemsException(); }

            return grouped.Values.Where(x => x.Count == 2).Select(x =>
            {
                var toUpdateEntry = new MergeToUpdateEntry<T>();

                if (oldCollectionLookup.Contains(x[0]))
                {
                    toUpdateEntry.OldItem = x[0];
                    toUpdateEntry.NewItem = x[1];
                }
                else
                {
                    toUpdateEntry.OldItem = x[1];
                    toUpdateEntry.NewItem = x[0];
                }

                return toUpdateEntry;
            }).ToList();
        }
    }
}