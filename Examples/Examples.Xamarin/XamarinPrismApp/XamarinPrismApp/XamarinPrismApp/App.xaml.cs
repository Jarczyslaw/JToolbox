using Acr.UserDialogs;
using JToolbox.XamarinForms.Core.Abstraction;
using JToolbox.XamarinForms.Core.Navigation;
using JToolbox.XamarinForms.Dialogs;
using JToolbox.XamarinForms.Logging;
using JToolbox.XamarinForms.Perms;
using Prism;
using Prism.Ioc;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPrismApp.DataAccess;
using XamarinPrismApp.Services;
using XamarinPrismApp.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace XamarinPrismApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
            Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl-PL");
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.StartNavigationViewModel<MainViewModel>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterDependencies(containerRegistry);
            RegisterViews(containerRegistry);
        }

        private void RegisterDependencies(IContainerRegistry containerRegistry)
        {
            if (!containerRegistry.IsRegistered<IApplicationCoreService>())
            {
                throw new Exception($"{nameof(IApplicationCoreService)} is not registered");
            }

            RegisterLogger(containerRegistry);
            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterSingleton<IDialogsService, DialogsService>();
            containerRegistry.RegisterSingleton<IAppSettings, AppSettings>();
            containerRegistry.RegisterSingleton<IPermsService, PermsService>();
            containerRegistry.RegisterSingleton<IDataAccessService, DataAccessService>();
        }

        private void RegisterLogger(IContainerRegistry containerRegistry)
        {
            var appConfig = Container.Resolve<IApplicationCoreService>();
            var logPath = Path.Combine(appConfig.PublicExternalFolder, AppInfo.Name);
            var loggerService = LoggerService.CreateSplittedConfiguration(logPath);
            containerRegistry.RegisterInstance<ILoggerService>(loggerService);
        }

        private void RegisterViews(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            NavigationMapper.Instance.Register(containerRegistry, Assembly.GetExecutingAssembly());
        }
    }
}