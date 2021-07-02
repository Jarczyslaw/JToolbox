using System.Threading.Tasks;
using Unity;

namespace JToolbox.WinForms.MVP.Unity
{
    public abstract class UnityBootstrapper
    {
        public async Task<object> Start<TMainPresenter, TMainView>(IUnityContainer container, PresenterFactory presenterFactory, string viewKey)
            where TMainPresenter : Presenter<TMainView>
            where TMainView : class, IView
        {
            if (!container.IsRegistered(presenterFactory.GetType()))
            {
                container.RegisterInstance(presenterFactory);
            }

            var presenter = await presenterFactory.Create<TMainPresenter, TMainView>(viewKey);
            return await presenter.Show();
        }

        public abstract void RegisterDependencies(IUnityContainer container);
    }
}