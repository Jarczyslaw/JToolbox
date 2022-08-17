using JToolbox.Misc.Serializers;
using JToolbox.XamarinForms.Core.Abstraction;
using JToolbox.XamarinForms.Core.Awareness;
using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using JToolbox.XamarinForms.Logging;
using JToolbox.XamarinForms.Perms;
using JToolbox.XamarinForms.Updater;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XamarinPrismApp.ViewModels
{
    public class MainViewModel : ViewModelBase, IOnBackButtonAware, IOnAppearedAware
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
            LogPath = "Log path: " + loggingService.GetAllLogFolders()[0];
        }

        public DelegateCommand AccelerometerCommand => new DelegateCommand(async () => await Navigate<AccelerometerViewModel>());

        public string AppVersion => $"Version: {AppInfo.Version}";

        public DelegateCommand CameraCommand => new DelegateCommand(async () => await Navigate<CameraViewModel>());

        public string DeviceId
        {
            get => deviceId;
            set => SetProperty(ref deviceId, value);
        }

        public DelegateCommand DialogsCommand => new DelegateCommand(async () => await Navigate<DialogsViewModel>());

        public DelegateCommand FileSystemCommand => new DelegateCommand(async () => await Navigate<FileSystemViewModel>());

        public DelegateCommand KillCommand => new DelegateCommand(async () => await KillPrompt());

        public DelegateCommand LocalStorageCommand => new DelegateCommand(async () => await Navigate<LocalStorageViewModel>());

        public DelegateCommand LogCommand => new DelegateCommand(async () =>
        {
            try
            {
                loggingService.Debug("DEBUG");
                loggingService.Error("ERROR");

                throw new Exception("TEST EXCEPTION");
            }
            catch (Exception exc)
            {
                loggingService.Error(exc, "Error occured: ");
                await dialogsService.Error(exc.Message);
            }

            dialogsService.Toast("Success");
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

        public DelegateCommand TestCommand => new DelegateCommand(() => Test());

        public DelegateCommand UpdateCommand => new DelegateCommand(() => Update());

        public async Task OnAppeared()
        {
            var result = await permissionsService.CheckAndRequest(new List<Permissions.BasePermission>
            {
                new Permissions.StorageRead(),
                new Permissions.StorageWrite()
            });

            if (!result)
            {
                await dialogsService.Information("Permissions not granted. App will be closed");
                applicationCoreService.Kill();
            }
            loggingService.Info("App started");
        }

        public async Task<bool> OnBackButton()
        {
            await KillPrompt();
            return false;
        }

        public async void Test()
        {
            try
            {
                dialogsService.Toast("TEST");
            }
            catch (Exception exc)
            {
                await dialogsService.Error(exc.Message);
            }
        }

        public async void Update()
        {
            try
            {
                var updater = new AppUpdater(applicationCoreService);
                var settings = new UpdaterSettings
                {
                    Password = "xamarinTest",
                    UserName = "xamarinTest",
                    Url = "http://apps.jtjt.pl/xamarinUpdaterTest"
                };

                dialogsService.UserDialogs.ShowLoading("Checking updates");

                await updater.Update(settings, async (oldVersion, newVersion) =>
                {
                    dialogsService.UserDialogs.HideLoading();
                    await dialogsService.Information($"Old version: {oldVersion}, new version: {newVersion}");

                    dialogsService.UserDialogs.ShowLoading("Downloading update");
                });
            }
            catch (Exception exc)
            {
                await dialogsService.Error(exc.Message);
            }
            finally
            {
                dialogsService.UserDialogs.HideLoading();
            }
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