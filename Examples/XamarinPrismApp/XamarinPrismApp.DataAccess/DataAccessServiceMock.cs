using System;
using System.Collections.Generic;
using System.Linq;

namespace XamarinPrismApp.DataAccess
{
    public class DataAccessServiceMock : IDataAccessService
    {
        private readonly List<User> users = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Jack",
                UpdateDate = DateTime.Now.AddDays(-1)
            },
            new User
            {
                Id = 2,
                Name = "Martin",
                UpdateDate = DateTime.Now.AddDays(-2)
            },
            new User
            {
                Id = 3,
                Name = "Tim",
                UpdateDate = DateTime.Now.AddDays(-3)
            }
        };

        public void AddUser(User user)
        {
            var newUser = new User(user);
            newUser.Id = GetNextId();
            users.Add(newUser);
        }

        public void DeleteUser(int id)
        {
            var user = users.Find(u => u.Id == id);
            if (user != null)
            {
                users.Remove(user);
            }
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public void UpdateUser(int id, User user)
        {
            var editedUser = users.Find(u => u.Id == id);
            if (editedUser != null)
            {
                editedUser.Name = user.Name;
                editedUser.UpdateDate = user.UpdateDate;
            }
        }

        private int GetNextId()
        {
            return users.Count == 0 ? 1 : users.Max(u => u.Id) + 1;
        }
    }
}