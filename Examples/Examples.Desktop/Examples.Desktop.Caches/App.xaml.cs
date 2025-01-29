using Examples.Desktop.Base;
using Prism.Ioc;
using System.Windows;

namespace Examples.Desktop.Caches
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return WindowManager.GetMainWindow("Caches");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}