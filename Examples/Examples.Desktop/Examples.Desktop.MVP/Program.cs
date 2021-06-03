using Examples.Desktop.MVP.Forms;
using Examples.Desktop.MVP.Presenters;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace Examples.Desktop.MVP
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var container = new UnityContainer();
            var factory = new AppPresenterFactory(container);
            var bootstrapper = new AppBootstrapper();
            bootstrapper.RegisterDependencies(container);
            await bootstrapper.Start<MainPresenter, IMainView>(container, factory);
        }
    }
}