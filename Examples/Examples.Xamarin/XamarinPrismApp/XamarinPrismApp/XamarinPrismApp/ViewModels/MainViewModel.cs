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
        private readonly IAppCore appCore;
        private readonly IDialogsService dialogsService;
        private readonly ILoggerService loggingService;
        private readonly IPermsService permissionsService;
        private string deviceId;
        private string logPath;

        public MainViewModel(ILoggerService loggingService, IAppCore appCore, IDialogsService dialogsService, IPermsService permissionsService, INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";

            this.appCore = appCore;
            this.dialogsService = dialogsService;
            this.permissionsService = permissionsService;
            this.loggingService = loggingService;

            DeviceId = "DeviceID: " + appCore.DeviceId;
            LogPath = "Log path: " + appCore.LogPath;
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

        public string LogPath
        {
            get => logPath;
            set => SetProperty(ref logPath, value);
        }

        public DelegateCommand NavigationCommand => new DelegateCommand(async () => await Navigate<NaviViewModel>());
        public DelegateCommand PermissionsCommand => new DelegateCommand(async () => await Navigate<PermissionsViewModel>());
        public DelegateCommand SettingsCommand => new DelegateCommand(async () => await Navigate<SettingsViewModel>());

        public DelegateCommand TestCommand => new DelegateCommand(async () =>
        {
            try
            {
                //var s = new PermsService();
                //var y = await s.Check(typeof(Permissions.StorageRead));
                loggingService.Debug("DEBUG");
                loggingService.Error("ERROR");
            }
            catch (Exception exc)
            {
                await dialogsService.Error(exc.Message);
            }
        });

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
                appCore.Kill();
            }
            loggingService.Info("App started");
        }

        private async Task KillPrompt()
        {
            var result = await dialogsService.QuestionYesNo("Do you really want to kill application?");
            if (result)
            {
                appCore.Kill();
            }
        }
    }
}