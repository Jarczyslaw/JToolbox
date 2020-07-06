using Plugin.Permissions.Abstractions;
using Prism.Mvvm;

namespace XamarinPrismApp.ViewModels
{
    public class PermissionsEntryViewModel : BindableBase
    {
        private string title;
        private Permission permission;

        public PermissionsEntryViewModel(Permission permission)
        {
            Permission = permission;
            Title = Permission.ToString();
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public Permission Permission
        {
            get => permission;
            set => SetProperty(ref permission, value);
        }
    }
}