using System.Threading.Tasks;
using Unity;

namespace JToolbox.WinForms.MVP.Unity
{
    public abstract class UnityBootstrapper
    {
        public async Task Start<TMainPresenter, TMainView>(IUnityContainer container, PresenterFactory presenterFactory)
            where TMainPresenter : Presenter<TMainView>
            where TMainView : class, IView
        {
            container.RegisterInstance(presenterFactory);
            RegisterDependencies(container);
            var presenter = await presenterFactory.Create<TMainPresenter, TMainView>();
            await presenter.Show();
        }

        protected abstract void RegisterDependencies(IUnityContainer container);
    }
}