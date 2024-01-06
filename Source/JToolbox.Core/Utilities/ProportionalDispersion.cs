using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Utilities
{
    public class ProportionalDispersion<T>
    {
        private readonly List<ProportionalDispersionItem<T>> items = new List<ProportionalDispersionItem<T>>();

        public int LowerBound { get; set; }

        public void AddItem(T item, decimal weight)
        {
            items.Add(new ProportionalDispersionItem<T>
            {
                Item = item,
                Weight = weight
            });
        }

        public List<ProportionalDispersionItem<T>> Calculate(int targetCount)
        {
            var totalWeight = items.Sum(x => x.Weight);
            if (totalWeight <= 0m || items.Count == 0) { return new List<ProportionalDispersionItem<T>>(); }

            foreach (var item in items)
            {
                item.Calculate(targetCount, totalWeight, LowerBound);
            }

            return items.Where(x => x.Count > 0)
                .ToList();
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