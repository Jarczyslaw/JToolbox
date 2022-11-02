using Examples.Desktop.Base;
using Prism.Ioc;
using System.Windows;

namespace Examples.Desktop.Others
{
    public partial class App
    {
        protected override Window CreateShell()
        {
            return WindowManager.GetMainWindow("Other examples");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}