using Prism.Mvvm;
using Xamarin.Essentials;

namespace XamarinPrismApp.ViewModels
{
    public class PermissionsEntryViewModel : BindableBase
    {
        private Permissions.BasePermission permission;
        private string title;

        public PermissionsEntryViewModel(Permissions.BasePermission permission)
        {
            Permission = permission;
            Title = permission.GetType().Name;
        }

        public Permissions.BasePermission Permission
        {
            get => permission;
            set => SetProperty(ref permission, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
    }
}