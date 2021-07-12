using Examples.Desktop.MVP.Forms;
using JToolbox.Desktop.Dialogs;
using JToolbox.WinForms.MVP;

namespace Examples.Desktop.MVP.Presenters
{
    public class MainPresenter : Presenter<IMainView>
    {
        private readonly IDialogsService dialogsService;

        public MainPresenter(PresenterFactory presenterFactory, IDialogsService dialogsService)
            : base(presenterFactory)
        {
            this.dialogsService = dialogsService;
        }

        public override void Attach(IMainView view)
        {
            base.Attach(view);
            View.OnPass += BaseView_OnPass;
        }

        protected override void Detach()
        {
            View.OnPass -= BaseView_OnPass;
            base.Detach();
        }

        private void BaseView_OnPass(bool modal, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                dialogsService.ShowError("Value can not be empty");
                return;
            }

            var presenter = presenterFactory.Create<ResultPresenter, IResultView>(ViewKeys.Result, value);
            if (modal)
            {
                var result = (string)presenter.ShowAsModal();
                if (string.IsNullOrEmpty(result))
                {
                    dialogsService.ShowInfo("Cancelled");
                }
                else
                {
                    dialogsService.ShowInfo("Retured value: " + result);
                }
            }
            else
            {
                presenter.Show();
            }
        }
    }
}