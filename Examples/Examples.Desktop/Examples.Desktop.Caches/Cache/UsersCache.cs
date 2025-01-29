using JToolbox.Core.Cache;
using JToolbox.Core.TimeProvider;
using System;

namespace Examples.Desktop.Caches.Cache
{
    internal class UsersCache : DictionaryCache<User>
    {
        public UsersCache(
            IDictionariesCacheDataSource dictionariesCacheDataSource,
            ITimeProvider timeProvider)
            : base(dictionariesCacheDataSource, timeProvider, null)
        {
        }

        protected override DateTime GetModificationDateFromItem(User item) => item.ModificationDate;

        protected override bool RemoveCondition(User item) => false;
    }
}