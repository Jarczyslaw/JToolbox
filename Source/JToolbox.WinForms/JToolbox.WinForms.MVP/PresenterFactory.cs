using System;

namespace JToolbox.WinForms.MVP
{
    public abstract class PresenterFactory
    {
        public TPresenter Create<TPresenter, TView>(string viewKey, object input = null)
            where TPresenter : Presenter<TView>
            where TView : class, IView
        {
            var view = ResolveView(viewKey);
            if (view == null)
            {
                throw new ArgumentException($"No view found for {viewKey} key");
            }

            if (view is TView presenterView)
            {
                var presenter = ResolvePresenter<TPresenter, TView>();
                presenter.Attach(presenterView);
                presenter.Initialize(input);
                return presenter;
            }
            else
            {
                throw new ArgumentException("Found view does not match requested presenter");
            }
        }

        public void Show<TPresenter, TView>(string viewKey, object input = null)
            where TPresenter : Presenter<TView>
            where TView : class, IView
        {
            var presenter = Create<TPresenter, TView>(viewKey, input);
            presenter.Show();
        }

        public object ShowAsModal<TPresenter, TView>(string viewKey, object input = null)
            where TPresenter : Presenter<TView>
            where TView : class, IView
        {
            var presenter = Create<TPresenter, TView>(viewKey, input);
            return presenter.ShowAsModal();
        }

        protected abstract IView ResolveView(string viewKey);

        protected abstract TPresenter ResolvePresenter<TPresenter, TView>()
            where TPresenter : Presenter<TView>
            where TView : class, IView;
    }
}