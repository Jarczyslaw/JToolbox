using System.Collections.Generic;

namespace XamarinPrismApp.DataAccess
{
    public interface IDataAccessService
    {
        List<User> GetUsers();

        void AddUser(User user);

        void UpdateUser(int id, User user);

        void DeleteUser(int id);
    }
}