using Prism.Mvvm;
using System;

namespace XamarinPrismApp.ViewModels
{
    public class PermissionsEntryViewModel : BindableBase
    {
        private Type permissionType;
        private string title;

        public PermissionsEntryViewModel(Type permissionType)
        {
            Permission = permissionType;
            Title = Permission.Name;
        }

        public Type Permission
        {
            get => permissionType;
            set => SetProperty(ref permissionType, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
    }
}