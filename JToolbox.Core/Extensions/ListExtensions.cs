using System;
using System.Collections.Generic;

namespace JToolbox.Core.Extensions
{
    public static class ListExtensions
    {
        public static List<List<T>> ChunkBy<T>(this List<T> list, int chunkSize)
        {
            var result = new List<List<T>>();
            for (int i = 0; ; i++)
            {
                var index = i * chunkSize;
                if (index >= list.Count)
                {
                    break;
                }
                result.Add(list.GetRange(index, Math.Min(chunkSize, list.Count - index)));
            }
            return result;
        }

        public static List<List<T>> ChunkInto<T>(this List<T> list, int chunksCount)
        {
            var result = new List<List<T>>();
            var chunkSize = (float)list.Count / chunksCount;
            if (chunkSize < 1f)
            {
                chunkSize = 1f;
            }

            var itemsAdded = 0;
            for (int i = 0; i < chunksCount; i++)
            {
                var index = (int)Math.Round(i * chunkSize);
                var itemsToAdd = (int)Math.Round((i + 1) * chunkSize) - itemsAdded;
                result.Add(list.GetRange(index, itemsToAdd));
                itemsAdded += itemsToAdd;
            }
            return result;
        }
    }
}