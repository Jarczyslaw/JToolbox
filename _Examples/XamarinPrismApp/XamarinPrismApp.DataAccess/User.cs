using System;

namespace XamarinPrismApp.DataAccess
{
    public class User
    {
        public User()
        {
        }

        public User(User user)
        {
            Name = user.Name;
            UpdateDate = user.UpdateDate;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}