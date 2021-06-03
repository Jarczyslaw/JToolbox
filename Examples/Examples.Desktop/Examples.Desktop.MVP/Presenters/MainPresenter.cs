using Examples.Desktop.MVP.Forms;
using JToolbox.Desktop.Dialogs;
using JToolbox.WinForms.MVP;
using System.Threading.Tasks;

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

        protected override Task OnAttach()
        {
            View.OnPass += BaseView_OnPass;
            return base.OnAttach();
        }

        protected override Task OnDetach()
        {
            View.OnPass -= BaseView_OnPass;
            return base.OnDetach();
        }

        private async void BaseView_OnPass(bool modal, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                dialogsService.ShowError("Value can not be empty");
                return;
            }

            var presenter = await presenterFactory.Create<ResultPresenter, IResultView>(value);
            string result;
            if (modal)
            {
                result = (string)await presenter.ShowAsModal();
            }
            else
            {
                result = (string)await presenter.Show();
            }

            if (string.IsNullOrEmpty(result))
            {
                dialogsService.ShowInfo("Cancelled");
            }
            else
            {
                dialogsService.ShowInfo("Retured value: " + result);
            }
        }
    }
}