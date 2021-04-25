using Examples.Desktop.Base;
using Prism.Ioc;
using System.Windows;

namespace Examples.Desktop.WCF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return WindowManager.GetMainWindow("WCF");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}