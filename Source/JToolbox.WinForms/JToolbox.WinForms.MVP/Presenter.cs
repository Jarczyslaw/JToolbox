using System;
using System.Threading.Tasks;

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

        public TaskCompletionSource<object> CompletionSource { get; } = new TaskCompletionSource<object>();

        public TView View { get; private set; }

        public object Input { get; protected set; }

        public object Output { get; protected set; }

        public async Task Attach(TView view)
        {
            if (View != null)
            {
                throw new Exception("View is already attached");
            }

            View = view;
            View.OnViewClosed += View_OnClosed;
            await OnAttach();
        }

        protected virtual Task OnAttach()
        {
            return Task.CompletedTask;
        }

        public async Task Initialize(object input)
        {
            Input = input;
            await OnInitialize(Input);
        }

        protected virtual Task OnInitialize(object input)
        {
            return Task.CompletedTask;
        }

        private async Task Detach()
        {
            if (View == null)
            {
                throw new Exception("View is already attached");
            }

            View.OnViewClosed -= View_OnClosed;
            await OnDetach();
            View = default;
        }

        protected virtual Task OnDetach()
        {
            return Task.CompletedTask;
        }

        public Task<object> Show()
        {
            View.ShowView();
            return CompletionSource.Task;
        }

        public Task<object> ShowAsModal()
        {
            View.ShowViewAsModal();
            return CompletionSource.Task;
        }

        public void Close(object output = null)
        {
            Output = output;
            View.CloseView();
        }

        private async void View_OnClosed()
        {
            await Detach();
            CompletionSource.SetResult(Output);
        }
    }
}