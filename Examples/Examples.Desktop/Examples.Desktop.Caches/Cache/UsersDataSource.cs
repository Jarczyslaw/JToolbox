using JToolbox.Core.Cache;
using JToolbox.Core.TimeProvider;
using JToolbox.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Examples.Desktop.Caches.Cache
{
    internal class UsersDataSource : IDictionariesCacheDataSource
    {
        private static readonly object _lock = new object();
        private readonly ITimeProvider _timeProvider;
        private readonly List<User> _users = new List<User>();
        private readonly int _usersLimit = 1_000;
        private int _counter;

        public UsersDataSource(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public List<User> Users => _users.ConvertAll(x => x.Clone());

        public void AddAndUpdate()
        {
            lock (_lock)
            {
                if (_users.Count >= _usersLimit)
                {
                    _users.Clear();
                }

                _users.Add(CreateNewUser());
                _users.Add(CreateNewUser());

                User userToUpdate = _users.FirstOrDefault(x => !x.IsUpdated);
                if (userToUpdate != null)
                {
                    userToUpdate.IsUpdated = true;
                }
            }
        }

        public DictionariesContainer GetFullData()
        {
            lock (_lock)
            {
                return DictionariesContainer.Create(Users);
            }
        }

        public DictionariesContainer GetModifiedData(int? id, DateTime? modificationDate)
        {
            lock (_lock)
            {
                List<User> result = Users.Where(x => x.Id > id || x.ModificationDate > modificationDate)
                    .ToList();

                return DictionariesContainer.Create(result);
            }
        }

        public (int? id, DateTime? modificationDate) GetUpdateIndicators()
        {
            lock (_lock)
            {
                int? maxId = _users.Max(x => (int?)x.Id);
                DateTime? maxModificationDate = _users.Max(x => (DateTime?)x.ModificationDate);

                return (maxId, maxModificationDate);
            }
        }

        public bool ValidateUsers(Dictionary<int, User> users)
        {
            lock (_lock)
            {
                return _users.Count == users.Count
                    && _users.Count(x => x.IsUpdated) == users.Values.Count(x => x.IsUpdated);
            }
        }

        private User CreateNewUser()
        {
            int id = Interlocked.Increment(ref _counter);

            return new User
            {
                Name = $"User_{id}",
                ModificationDate = _timeProvider.Now,
                Id = id,
            };
        }
    }
}