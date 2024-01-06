using System;

namespace JToolbox.Core.Utilities
{
    public class ProportionalDispersionItem<T>
    {
        public int Count { get; set; }

        public T Item { get; set; }

        public decimal Weight { get; set; }

        public int Calculate(int targetCount, decimal totalWeight, int lowerBound)
        {
            var count = (int)Math.Round(Weight / totalWeight * targetCount);
            if (count < lowerBound) { count = lowerBound; }

            return Count = count;
        }
    }
}