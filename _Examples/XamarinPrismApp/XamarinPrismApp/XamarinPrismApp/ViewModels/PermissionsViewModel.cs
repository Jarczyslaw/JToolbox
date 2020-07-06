using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using JToolbox.XamarinForms.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace XamarinPrismApp.ViewModels
{
    public class PermissionsViewModel : ViewModelBase
    {
        private readonly IPermissionsService permissionsService;
        private readonly IDialogsService dialogsService;
        private PermissionsEntryViewModel selectedPermission;

        public PermissionsViewModel(IDialogsService dialogsService, IPermissionsService permissionsService, INavigationService navigationService)
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
            var result = await permissionsService.CheckPermission(SelectedPermission.Permission);
            ShowResult(result);
        });

        public DelegateCommand RequestPermissionCommand => new DelegateCommand(async () =>
        {
            var result = await permissionsService.CheckAndRequestPermission(SelectedPermission.Permission);
            ShowResult(result);
        });

        private void InitializePermissions()
        {
            Permissions = new ObservableCollection<PermissionsEntryViewModel>
            {
                new PermissionsEntryViewModel(Permission.Camera),
                new PermissionsEntryViewModel(Permission.Location),
                new PermissionsEntryViewModel(Permission.Storage),
                new PermissionsEntryViewModel(Permission.Phone),
                new PermissionsEntryViewModel(Permission.Calendar),
            };
            SelectedPermission = Permissions.First();
        }

        private void ShowResult(PermissionStatus result)
        {
            dialogsService.Toast($"Permision status: {result.ToString()}");
        }
    }
}