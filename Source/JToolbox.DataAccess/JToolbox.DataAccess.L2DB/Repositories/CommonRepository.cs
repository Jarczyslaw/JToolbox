using JToolbox.DataAccess.L2DB.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.DataAccess.L2DB.Repositories
{
    public abstract class CommonRepository
    {
        public List<T> PrepareForMerge<T>(List<T> items, Action<T> action)
            where T : IBaseEntity
        {
            if (items == null) { return new List<T>(); }

            int counter = -1;
            items.Where(x => x.Id <= 0)
                .ToList()
                .ForEach(x =>
                {
                    x.Id = counter;
                    counter--;
                });

            items.ForEach(action);
            return items;
        }
    }
}