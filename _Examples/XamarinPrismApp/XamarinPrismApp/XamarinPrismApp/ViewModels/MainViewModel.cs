﻿using JToolbox.Core.Abstraction;
using JToolbox.XamarinForms.Core.Abstraction;
using JToolbox.XamarinForms.Core.Awareness;
using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using JToolbox.XamarinForms.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using JToolbox.XamarinForms.Core.Navigation;

namespace XamarinPrismApp.ViewModels
{
    public class MainViewModel : ViewModelBase, IOnBackButtonAware, IOnShownAware
    {
        private readonly IAppCore appCore;
        private readonly IDialogsService dialogsService;
        private readonly IPermissionsService permissionsService;
        private readonly ILoggerService loggingService;
        private string deviceId;
        private string logPath;

        public MainViewModel(ILoggerService loggingService, IAppCore appCore, IDialogsService dialogsService, IPermissionsService permissionsService, INavigationService navigationService)
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

        public string LogPath
        {
            get => logPath;
            set => SetProperty(ref logPath, value);
        }

        public string DeviceId
        {
            get => deviceId;
            set => SetProperty(ref deviceId, value);
        }

        public DelegateCommand NavigationCommand => new DelegateCommand(async () => await Navigate<NaviViewModel>());

        public DelegateCommand DialogsCommand => new DelegateCommand(async () => await Navigate<DialogsViewModel>());

        public DelegateCommand SettingsCommand => new DelegateCommand(async () => await Navigate<SettingsViewModel>());

        public DelegateCommand PermissionsCommand => new DelegateCommand(async () => await Navigate<PermissionsViewModel>());

        public DelegateCommand AccelerometerCommand => new DelegateCommand(async () => await Navigate<AccelerometerViewModel>());

        public DelegateCommand LocalStorageCommand => new DelegateCommand(async () => await Navigate<LocalStorageViewModel>());

        public DelegateCommand TestCommand => new DelegateCommand(() =>
        {
        });

        public DelegateCommand KillCommand => new DelegateCommand(async () => await KillPrompt());

        public async Task<bool> OnBackButton()
        {
            await KillPrompt();
            return false;
        }

        public async Task OnShown()
        {
            var result = await permissionsService.CheckAndRequestPermission(Permission.Storage);
            if (result != PermissionStatus.Granted)
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