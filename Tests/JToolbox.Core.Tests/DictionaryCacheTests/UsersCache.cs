using JToolbox.Core.Cache;
using JToolbox.Core.TimeProvider;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DictionaryCacheTests
{
    internal class UsersCache : DictionaryCache<User>
    {
        public UsersCache(
            IDictionariesCacheDataSource dictionariesCacheDataSource,
            ITimeProvider timeProvider)
            : base(dictionariesCacheDataSource, timeProvider, null)
        {
        }

        public Dictionary<int, User> GetUsers()
        {
            Dictionary<int, User> users = new Dictionary<int, User>();
            GetRefreshedData(x => users = x);
            return users;
        }

        protected override DateTime GetModificationDateFromItem(User item) => item.ModificationDate;

        protected override bool RemoveCondition(User item) => item.IsDeleted;
    }
}