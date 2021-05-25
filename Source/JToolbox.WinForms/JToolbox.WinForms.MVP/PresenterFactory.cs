using System.Threading.Tasks;

namespace JToolbox.WinForms.MVP
{
    public abstract class PresenterFactory
    {
        public async Task<TPresenter> Create<TPresenter, TView>()
            where TPresenter : Presenter<TView>
            where TView : class, IView
        {
            var view = ResolveView<TPresenter, TView>();
            var presenter = ResolvePresenter<TPresenter, TView>();
            await presenter.Attach(view);
            return presenter;
        }

        protected abstract TView ResolveView<TPresenter, TView>()
            where TPresenter : Presenter<TView>
            where TView : class, IView;

        protected abstract TPresenter ResolvePresenter<TPresenter, TView>()
            where TPresenter : Presenter<TView>
            where TView : class, IView;
    }
}