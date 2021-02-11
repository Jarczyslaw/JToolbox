using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using JToolbox.XamarinForms.Perms;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace XamarinPrismApp.ViewModels
{
    public class PermissionsViewModel : ViewModelBase
    {
        private readonly IPermsService permissionsService;
        private readonly IDialogsService dialogsService;
        private PermissionsEntryViewModel selectedPermission;

        public PermissionsViewModel(IDialogsService dialogsService, IPermsService permissionsService, INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Permissions";

            this.permissionsService = permissionsService;
            this.dialogsService = dialogsService;
            InitializePermissions();
        }

        public PermissionsEntryViewModel SelectedPermission
        {
            get => selectedPermission;
            set => SetProperty(ref selectedPermission, value);
        }

        public ObservableCollection<PermissionsEntryViewModel> Permissions { get; set; }

        public DelegateCommand CheckPermissionCommand => new DelegateCommand(async () =>
        {
            try
            {
                var result = await permissionsService.Check(SelectedPermission.Permission);
                ShowResult(result);
            }
            catch (Exception exc)
            {
                await dialogsService.Error(exc.Message);
            }
        });

        public DelegateCommand RequestPermissionCommand => new DelegateCommand(async () =>
        {
            try
            {
                var result = await permissionsService.CheckAndRequest(SelectedPermission.Permission);
                ShowResult(result);
            }
            catch (Exception exc)
            {
                await dialogsService.Error(exc.Message);
            }
        });

        private void InitializePermissions()
        {
            Permissions = new ObservableCollection<PermissionsEntryViewModel>
            {
                new PermissionsEntryViewModel(typeof(Permissions.Camera)),
                new PermissionsEntryViewModel(typeof(Permissions.LocationAlways)),
                new PermissionsEntryViewModel(typeof(Permissions.LocationWhenInUse)),
                new PermissionsEntryViewModel(typeof(Permissions.StorageRead)),
                new PermissionsEntryViewModel(typeof(Permissions.StorageWrite)),
                new PermissionsEntryViewModel(typeof(Permissions.Phone)),
                new PermissionsEntryViewModel(typeof(Permissions.CalendarRead)),
            };
            SelectedPermission = Permissions.First();
        }

        private void ShowResult(PermissionStatus result)
        {
            dialogsService.Toast($"Permision status: {result}");
        }
    }
}