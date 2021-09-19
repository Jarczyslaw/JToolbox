using Prism.Mvvm;
using System;
using XamarinPrismApp.DataAccess;

namespace XamarinPrismApp.Models
{
    public class UserViewModel : BindableBase
    {
        private readonly User user;

        public UserViewModel(User user)
        {
            this.user = user;
        }

        public DateTime Date
        {
            get => user.UpdateDate;
            set
            {
                user.UpdateDate = value;
                RaisePropertyChanged(nameof(Date));
            }
        }

        public int Id
        {
            get => user.Id;
            set
            {
                user.Id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => user.Name;
            set
            {
                user.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
    }
}