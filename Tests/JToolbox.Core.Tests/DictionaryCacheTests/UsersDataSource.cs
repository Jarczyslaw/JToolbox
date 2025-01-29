using JToolbox.Core.Cache;
using JToolbox.Core.TimeProvider;
using JToolbox.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Tests.DictionaryCacheTests
{
    internal class UsersDataSource : IDictionariesCacheDataSource
    {
        private readonly ITimeProvider _timeProvider;
        private readonly List<User> _users = new List<User>();

        public UsersDataSource(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;

            Add(new User { Name = "John" });
            Add(new User { Name = "Mark" });
            Add(new User { Name = "Kevin" });
        }

        public List<User> Users => _users.ConvertAll(x => x.Clone());

        public void Add(User user)
        {
            user.ModificationDate = _timeProvider.Now;
            user.Id = (_users.Max(x => (int?)x.Id) ?? 0) + 1;

            _users.Add(user);
        }

        public bool AreReferencesDifferent(Dictionary<int, User> users)
        {
            foreach (User user in _users)
            {
                if (ReferenceEquals(users[user.Id], user)) { return false; }
            }

            return true;
        }

        public void Delete(int id)
        {
            User existingUser = _users.First(x => x.Id == id);

            _users.Remove(existingUser);
        }

        public void DeleteAll() => _users.Clear();

        public DictionariesContainer GetFullData() => DictionariesContainer.Create(Users);

        public DictionariesContainer GetModifiedData(int? id, DateTime? modificationDate)
        {
            List<User> result = Users.Where(x => x.Id > id || x.ModificationDate > modificationDate)
                .ToList();

            return DictionariesContainer.Create(result);
        }

        public (int? id, DateTime? modificationDate) GetUpdateIndicators()
        {
            int? maxId = _users.Max(x => (int?)x.Id);
            DateTime? maxModificationDate = Users.Max(x => (DateTime?)x.ModificationDate);

            return (maxId, maxModificationDate);
        }

        public void MarkAsDeleted(int id)
        {
            User existingUser = _users.First(x => x.Id == id);

            existingUser.IsDeleted = true;
            existingUser.ModificationDate = _timeProvider.Now;
        }

        public void Update(User user)
        {
            User existingUser = _users.First(x => x.Id == user.Id);

            existingUser.ModificationDate = _timeProvider.Now;
            existingUser.Name = user.Name;
            existingUser.IsDeleted = user.IsDeleted;
        }
    }
}