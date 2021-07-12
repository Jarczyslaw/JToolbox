using Unity;

namespace JToolbox.WinForms.MVP.Unity
{
    public abstract class UnityBootstrapper
    {
        public void Start<TMainPresenter, TMainView>(IUnityContainer container, PresenterFactory presenterFactory, string viewKey, object input = null)
            where TMainPresenter : Presenter<TMainView>
            where TMainView : class, IView
        {
            if (!container.IsRegistered(presenterFactory.GetType()))
            {
                container.RegisterInstance(presenterFactory);
            }

            presenterFactory.Show<TMainPresenter, TMainView>(viewKey, input);
        }

        public abstract void RegisterDependencies(IUnityContainer container);
    }
}