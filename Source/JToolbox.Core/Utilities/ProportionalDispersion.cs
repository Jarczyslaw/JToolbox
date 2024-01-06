using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Utilities
{
    public class ProportionalDispersion<T>
    {
        public List<ProportionalDispersionItem<T>> Items { get; set; } = new List<ProportionalDispersionItem<T>>();

        public int LowerBound { get; set; }

        public void AddItem(T item, decimal weight)
        {
            Items.Add(new ProportionalDispersionItem<T>
            {
                Item = item,
                Weight = weight
            });
        }

        public List<ProportionalDispersionItem<T>> Calculate(int targetCount)
        {
            Items.ForEach(x => x.Count = 0);

            var totalWeight = Items.Sum(x => x.Weight);
            if (totalWeight <= 0m || Items.Count == 0) { return new List<ProportionalDispersionItem<T>>(); }

            var currentCountSum = 0;
            var orderedItems = Items.OrderBy(x => x.Weight);
            var lastItem = orderedItems.Last();
            foreach (var item in orderedItems)
            {
                var calculated = item.Calculate(targetCount, totalWeight, LowerBound);
                if (item == lastItem)
                {
                    item.Count = Math.Max(calculated, targetCount - currentCountSum);
                }
                currentCountSum += calculated;
            }

            return Items.ToList();
        }

        public List<T> GetQueuedItems(List<ProportionalDispersionItem<T>> dispersedItems)
        {
            var result = new List<T>();
            foreach (var dispersedItem in dispersedItems)
            {
                result.AddRange(Enumerable.Repeat(dispersedItem.Item, dispersedItem.Count));
            }
            return result;
        }
    }
}