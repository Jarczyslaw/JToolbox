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
        private readonly IDialogsService dialogsService;
        private readonly IPermsService permissionsService;
        private PermissionsEntryViewModel selectedPermission;

        public PermissionsViewModel(IDialogsService dialogsService, IPermsService permissionsService, INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Permissions";

            this.permissionsService = permissionsService;
            this.dialogsService = dialogsService;
            InitializePermissions();
        }

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

        public ObservableCollection<PermissionsEntryViewModel> Permissions { get; set; }

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

        public PermissionsEntryViewModel SelectedPermission
        {
            get => selectedPermission;
            set => SetProperty(ref selectedPermission, value);
        }

        private void InitializePermissions()
        {
            Permissions = new ObservableCollection<PermissionsEntryViewModel>
            {
                new PermissionsEntryViewModel(new Permissions.Camera()),
                new PermissionsEntryViewModel(new Permissions.LocationAlways()),
                new PermissionsEntryViewModel(new Permissions.LocationWhenInUse()),
                new PermissionsEntryViewModel(new Permissions.StorageRead()),
                new PermissionsEntryViewModel(new Permissions.StorageWrite()),
                new PermissionsEntryViewModel(new Permissions.Phone()),
                new PermissionsEntryViewModel(new Permissions.CalendarRead()),
            };
            SelectedPermission = Permissions.First();
        }

        private void ShowResult(PermissionStatus result)
        {
            dialogsService.Toast($"Permision status: {result}");
        }
    }
}