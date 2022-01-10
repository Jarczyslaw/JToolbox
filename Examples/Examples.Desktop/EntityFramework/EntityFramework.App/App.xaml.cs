using EntityFramework.App.Views;
using EntityFramework.BusinessLogic;
using JToolbox.Desktop.Dialogs;
using System.Windows;
using Unity;

namespace EntityFramework.App
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = CreateContainer();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            container.RegisterInstance<IUnityContainer>(container);
            container.RegisterSingleton<IDialogsService, DialogsService>();
            container.RegisterSingleton<IBusinessService, BusinessService>();
            return container;
        }
    }
}