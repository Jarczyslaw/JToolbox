using Examples.Desktop.Base;
using Prism.Ioc;
using System.Windows;

namespace Examples.Desktop.SysInformation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return WindowManager.GetMainWindow("System information");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}