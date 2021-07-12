using System;

namespace JToolbox.WinForms.MVP
{
    public abstract class Presenter<TView>
        where TView : class, IView
    {
        protected readonly PresenterFactory presenterFactory;

        protected Presenter(PresenterFactory presenterFactory)
        {
            this.presenterFactory = presenterFactory;
        }

        public TView View { get; private set; }

        public object Input { get; protected set; }

        public object Output { get; protected set; }

        public virtual void Attach(TView view)
        {
            if (View != null)
            {
                throw new Exception("View is already attached");
            }

            View = view;
            View.OnViewClosed += View_OnClosed;
        }

        public virtual void Initialize(object input)
        {
            Input = input;
        }

        protected virtual void Detach()
        {
            if (View == null)
            {
                throw new Exception("View is already attached");
            }

            View.OnViewClosed -= View_OnClosed;
            View = default;
        }

        public void Show()
        {
            View.ShowView();
        }

        public object ShowAsModal()
        {
            View.ShowViewAsModal();
            return Output;
        }

        public void Close(object output = null)
        {
            Output = output;
            View.CloseView();
        }

        private void View_OnClosed()
        {
            Detach();
        }
    }
}