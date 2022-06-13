using JToolbox.Core.Extensions;
using System;
using System.Collections.Generic;

namespace JToolbox.DataAccess.Common
{
    public static class SqlHelper
    {
        public static string CreateInCondition<T>(string columnName, List<T> values, int limit)
        {
            return CreateInCondition(columnName, values, limit, x => x.ToString());
        }

        public static string CreateInCondition<T>(string columnName, List<T> values, int limit, Func<T, string> valueFunc)
        {
            if (values == null || values.Count == 0) { return string.Empty; }

            var groups = new List<string>();

            foreach (var chunk in values.ChunkBy(limit))
            {
                var chunkValues = chunk.ConvertAll(x => valueFunc(x));
                var group = string.Join(", ", chunkValues);
                groups.Add($"{columnName} IN ({group})");
            }

            return $"({string.Join(" OR ", groups)})";
        }
    }
}