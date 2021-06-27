using JToolbox.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.DataAccess.SQLiteNet.Repositories
{
    public abstract class CommonRepository
    {
        protected void IncludeMany<T1, T2>(List<T1> collection, List<T2> toInclude,
            Func<T2, int> foreignKeySelector, Action<T1, List<T2>> includeAction)
            where T1 : IKey
        {
            var dict = toInclude.GroupBy(foreignKeySelector).ToDictionary(a => a.Key, a => a.ToList());
            foreach (var entity in collection)
            {
                var list = new List<T2>();
                if (dict.TryGetValue(entity.Id, out List<T2> value))
                {
                    list = value;
                }
                includeAction(entity, list);
            }
        }

        protected void IncludeOne<T1, T2>(List<T1> collection, List<T2> toInclude,
            Func<T2, int> foreignKeySelector, Action<T1, T2> includeAction)
            where T1 : IKey
        {
            IncludeMany(collection, toInclude, foreignKeySelector,
                (e1, e2) => includeAction(e1, e2.SingleOrDefault()));
        }
    }
}