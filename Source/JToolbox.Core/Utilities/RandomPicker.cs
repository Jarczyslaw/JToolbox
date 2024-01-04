using JToolbox.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Utilities
{
    public class RandomPicker<T>
    {
        private readonly IList<T> itemsToPick;
        private readonly List<int> predefinedIndexes;
        private List<int> indexes = new List<int>();

        public RandomPicker(IList<T> itemsToPick)
            : this(itemsToPick, null)
        {
        }

        public RandomPicker(IList<T> itemsToPick, List<int> predefinedIndexes)
        {
            this.itemsToPick = itemsToPick;
            this.predefinedIndexes = predefinedIndexes;

            if (this.itemsToPick == null || itemsToPick.Count == 0) { throw new ArgumentException($"Argument {itemsToPick} can not be empty"); }

            if (predefinedIndexes?.Count == 0) { throw new ArgumentException($"Argument {predefinedIndexes} can not be empty"); }

            if (predefinedIndexes?.Count > 0 && predefinedIndexes.Any(x => x >= itemsToPick.Count || x < 0))
            {
                throw new ArgumentException($"Invalid indexes in {predefinedIndexes}");
            }
        }

        public T GetNext()
        {
            if (indexes.Count == 0)
            {
                CreateIndexes();
            }

            var index = indexes[0];
            var result = itemsToPick[index];
            indexes.RemoveAt(0);

            return result;
        }

        private void CreateIndexes()
        {
            if (predefinedIndexes?.Count > 0)
            {
                indexes = predefinedIndexes.ToList();
            }
            else
            {
                for (int i = 0; i < itemsToPick.Count; i++)
                {
                    indexes.Add(i);
                }
                indexes.Shuffle();
            }
        }
    }
}