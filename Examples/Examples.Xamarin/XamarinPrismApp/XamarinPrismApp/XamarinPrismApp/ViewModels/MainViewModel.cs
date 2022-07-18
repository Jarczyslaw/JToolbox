using JToolbox.XamarinForms.Core.Abstraction;
using JToolbox.XamarinForms.Core.Awareness;
using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using JToolbox.XamarinForms.Logging;
using JToolbox.XamarinForms.Perms;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XamarinPrismApp.ViewModels
{
    public class MainViewModel : ViewModelBase, IOnBackButtonAware, IOnShownAware
    {
        private readonly IApplicationCoreService applicationCoreService;
        private readonly IDialogsService dialogsService;
        private readonly ILoggerService loggingService;
        private readonly IPermsService permissionsService;
        private string deviceId;
        private string logPath;

        public MainViewModel(ILoggerService loggingService, IApplicationCoreService applicationCoreService,
            IDialogsService dialogsService, IPermsService permissionsService, INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";

            this.applicationCoreService = applicationCoreService;
            this.dialogsService = dialogsService;
            this.permissionsService = permissionsService;
            this.loggingService = loggingService;

            DeviceId = "DeviceID: " + applicationCoreService.DeviceId;
            LogPath = "Log path: " + applicationCoreService.LogPath;
        }

        public DelegateCommand AccelerometerCommand => new DelegateCommand(async () => await Navigate<AccelerometerViewModel>());

        public string DeviceId
        {
            get => deviceId;
            set => SetProperty(ref deviceId, value);
        }

        public DelegateCommand DialogsCommand => new DelegateCommand(async () => await Navigate<DialogsViewModel>());

        public DelegateCommand KillCommand => new DelegateCommand(async () => await KillPrompt());

        public DelegateCommand LocalStorageCommand => new DelegateCommand(async () => await Navigate<LocalStorageViewModel>());

        public DelegateCommand LogCommand => new DelegateCommand(async () =>
        {
            try
            {
                loggingService.Debug("DEBUG");
                loggingService.Error("ERROR");

                dialogsService.Toast("Success");
            }
            catch (Exception exc)
            {
                await dialogsService.Error(exc.Message);
            }
        });

        public string LogPath
        {
            get => logPath;
            set => SetProperty(ref logPath, value);
        }

        public DelegateCommand NavigationCommand => new DelegateCommand(async () => await Navigate<NaviViewModel>());
        public DelegateCommand PermissionsCommand => new DelegateCommand(async () => await Navigate<PermissionsViewModel>());
        public DelegateCommand SecureLocalStorageCommand => new DelegateCommand(async () => await Navigate<SecureLocalStorageViewModel>());
        public DelegateCommand SettingsCommand => new DelegateCommand(async () => await Navigate<SettingsViewModel>());

        public async Task<bool> OnBackButton()
        {
            await KillPrompt();
            return false;
        }

        public async Task OnShown()
        {
            var result = await permissionsService.CheckAndRequest(new List<Type>
            {
                typeof(Permissions.StorageRead),
                typeof(Permissions.StorageWrite)
            });

            if (!result)
            {
                await dialogsService.Information("Permissions not granted. App will be closed");
                applicationCoreService.Kill();
            }
            loggingService.Info("App started");
        }

        private async Task KillPrompt()
        {
            var result = await dialogsService.QuestionYesNo("Do you really want to kill application?");
            if (result)
            {
                applicationCoreService.Kill();
            }
        }
    }
}