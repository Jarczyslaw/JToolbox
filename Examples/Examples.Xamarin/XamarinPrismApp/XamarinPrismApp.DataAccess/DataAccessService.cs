using JToolbox.XamarinForms.Core.Abstraction;
using LiteDB;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XamarinPrismApp.DataAccess
{
    public class DataAccessService : IDataAccessService
    {
        private readonly string databasePath;
        private readonly string usersCollectionName = "Users";

        public DataAccessService(IApplicationCoreService applicationCoreService)
        {
            databasePath = Path.Combine(applicationCoreService.InternalFolder, "database.db");
        }

        public void AddUser(User user)
        {
            using (var db = new LiteDatabase(databasePath))
            {
                db.GetCollection<User>(usersCollectionName)
                    .Insert(user);
            }
        }

        public void DeleteUser(int id)
        {
            using (var db = new LiteDatabase(databasePath))
            {
                db.GetCollection<User>(usersCollectionName)
                    .Delete(id);
            }
        }

        public List<User> GetUsers()
        {
            using (var db = new LiteDatabase(databasePath))
            {
                return db.GetCollection<User>(usersCollectionName)
                    .FindAll()
                    .ToList();
            }
        }

        public void UpdateUser(int id, User user)
        {
            using (var db = new LiteDatabase(databasePath))
            {
                var users = db.GetCollection<User>(usersCollectionName);
                var editedUser = users.FindById(id);
                if (editedUser != null)
                {
                    editedUser.Name = user.Name;
                    editedUser.UpdateDate = user.UpdateDate;
                    users.Update(editedUser);
                }
            }
        }
    }
}