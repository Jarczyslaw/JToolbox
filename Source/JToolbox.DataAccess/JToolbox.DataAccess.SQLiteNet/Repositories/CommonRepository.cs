using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.DataAccess.SQLiteNet.Repositories
{
    public abstract class CommonRepository
    {
        protected void IncludeMany<T1, T2>(List<T1> collection, List<T2> toInclude,
            Func<T1, int> collectionKeySelector, Func<T2, int> toIncludeKeySelector, Action<T1, List<T2>> includeAction)
        {
            var dict = toInclude.GroupBy(toIncludeKeySelector).ToDictionary(a => a.Key, a => a.ToList());
            foreach (var entity in collection)
            {
                var list = new List<T2>();
                var key = collectionKeySelector(entity);
                if (dict.TryGetValue(key, out List<T2> value))
                {
                    list = value;
                }
                includeAction(entity, list);
            }
        }

        protected void IncludeOne<T1, T2>(List<T1> collection, List<T2> toInclude,
            Func<T1, int> collectionKeySelector, Func<T2, int> toIncludeKeySelector, Action<T1, T2> includeAction)
        {
            IncludeMany(collection, toInclude,
                collectionKeySelector, toIncludeKeySelector, (e1, e2) => includeAction(e1, e2.SingleOrDefault()));
        }
    }
}