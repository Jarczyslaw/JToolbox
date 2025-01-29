using JToolbox.Core.Abstraction;
using JToolbox.Core.TimeProvider;
using JToolbox.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Cache
{
    public abstract class DictionaryCache<T> : AbstractDictionariesCache
        where T : class, IKey
    {
        protected DictionaryCache(
            IDictionariesCacheDataSource dictionariesCacheDataSource,
            ITimeProvider timeProvider,
            DictionariesCacheListener dictionariesCacheListener = null)
            : base(dictionariesCacheDataSource, timeProvider, dictionariesCacheListener)
        {
        }

        public UpdateIndicators GetRefreshedData(Action<Dictionary<int, T>> dataAction)
        {
            return GetRefreshedData(x => dataAction(x.GetDictionary<T>()));
        }

        protected override void ApplyDataToCache(DictionariesContainer updatedData)
        {
            _container.Submit<T>(updatedData, RemoveCondition);
        }

        protected abstract DateTime GetModificationDateFromItem(T item);

        protected override (int? newLastId, DateTime? newLastModificationDate) GetUpdateIndicatorsFromContainer(DictionariesContainer data)
        {
            Dictionary<int, T> dictionary = data.GetDictionary<T>();

            int? newLastId = dictionary.Keys.Max(x => (int?)x);
            DateTime? newModificationDate = dictionary.Values.Max(x => (DateTime?)GetModificationDateFromItem(x));

            return (newLastId, newModificationDate);
        }

        protected virtual bool RemoveCondition(T item) => false;
    }
}