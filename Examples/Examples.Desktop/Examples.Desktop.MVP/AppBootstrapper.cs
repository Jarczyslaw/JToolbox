using JToolbox.Desktop.Dialogs;
using JToolbox.WinForms.MVP.Unity;
using Unity;

namespace Examples.Desktop.MVP
{
    public class AppBootstrapper : UnityBootstrapper
    {
        protected override void RegisterDependencies(IUnityContainer container)
        {
            container.RegisterSingleton<IDialogsService, DialogsService>();
        }
    }
}