using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Helpers.CollectionsMerge
{
    public static class CollectionsMergeHelper
    {
        public static CollectionsMergeResult<T, T> Merge<T, TKey>(
            List<T> newItems,
            List<T> oldItems,
            Func<T, TKey> keySelector)
            => Merge(newItems, oldItems, keySelector, keySelector);

        public static CollectionsMergeResult<TNew, TOld> Merge<TNew, TOld, TKey>(
            List<TNew> newItems,
            List<TOld> oldItems,
            Func<TNew, TKey> newItemsKeySelector,
            Func<TOld, TKey> oldItemsKeySelector)
        {
            newItems = newItems ?? new List<TNew>();
            oldItems = oldItems ?? new List<TOld>();

            Dictionary<TKey, List<TNew>> newItemsDictionary = newItems
                .GroupBy(x => newItemsKeySelector(x))
                .ToDictionary(x => x.Key, x => x.ToList());

            Dictionary<TKey, List<TOld>> oldItemsDictionary = oldItems
                .GroupBy(x => oldItemsKeySelector(x))
                .ToDictionary(x => x.Key, x => x.ToList());

            CollectionsMergeResult<TNew, TOld> result = new CollectionsMergeResult<TNew, TOld>();

            FillItemsToAdd(
                newItemsDictionary,
                oldItemsDictionary,
                result);

            FillCommonItems(
                newItemsDictionary,
                oldItemsDictionary,
                result);

            FillItemsToDelete(
                newItemsDictionary,
                oldItemsDictionary,
                result);

            return result;
        }

        private static void FillCommonItems<TKey, TNew, TOld>(
            Dictionary<TKey, List<TNew>> newItemsDictionary,
            Dictionary<TKey, List<TOld>> oldItemsDictionary,
            CollectionsMergeResult<TNew, TOld> result)
        {
            foreach (TKey commonKey in newItemsDictionary.Keys.Intersect(oldItemsDictionary.Keys))
            {
                List<TNew> commonNewItems = newItemsDictionary[commonKey];
                List<TOld> commonOldItems = oldItemsDictionary[commonKey];

                for (int i = 0; i < Math.Max(commonNewItems.Count, commonOldItems.Count); i++)
                {
                    if (i < commonNewItems.Count && i < commonOldItems.Count)
                    {
                        result.ToUpdate.Add(new CollectionsMergeResultUpdateEntry<TNew, TOld>
                        {
                            NewItem = commonNewItems[i],
                            OldItem = commonOldItems[i]
                        });
                    }
                    else if (i < commonNewItems.Count)
                    {
                        result.ToAdd.Add(commonNewItems[i]);
                    }
                    else if (i < commonOldItems.Count)
                    {
                        result.ToDelete.Add(commonOldItems[i]);
                    }
                }
            }
        }

        private static void FillItemsToAdd<TNew, TOld, TKey>(
            Dictionary<TKey, List<TNew>> newItemsDictionary,
            Dictionary<TKey, List<TOld>> oldItemsDictionary,
            CollectionsMergeResult<TNew, TOld> result)
        {
            result.ToAdd.AddRange(newItemsDictionary.Keys
                .Except(oldItemsDictionary.Keys)
                .SelectMany(x => newItemsDictionary[x]));
        }

        private static void FillItemsToDelete<TKey, TNew, TOld>(
            Dictionary<TKey, List<TNew>> newItemsDictionary,
            Dictionary<TKey, List<TOld>> oldItemsDictionary,
            CollectionsMergeResult<TNew, TOld> result)
        {
            result.ToDelete.AddRange(oldItemsDictionary.Keys
                .Except(newItemsDictionary.Keys)
                .SelectMany(x => oldItemsDictionary[x]));
        }
    }
}