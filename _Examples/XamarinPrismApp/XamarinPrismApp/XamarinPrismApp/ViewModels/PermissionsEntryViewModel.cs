using Prism.Mvvm;
using System;

namespace XamarinPrismApp.ViewModels
{
    public class PermissionsEntryViewModel : BindableBase
    {
        private string title;
        private Type permissionType;

        public PermissionsEntryViewModel(Type permissionType)
        {
            Permission = permissionType;
            Title = Permission.Name;
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public Type Permission
        {
            get => permissionType;
            set => SetProperty(ref permissionType, value);
        }
    }
}