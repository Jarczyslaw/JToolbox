using System.Collections.Generic;

namespace XamarinPrismApp.DataAccess
{
    public interface IDataAccessService
    {
        void AddUser(User user);

        void DeleteUser(int id);

        List<User> GetUsers();

        void UpdateUser(int id, User user);
    }
}