using Examples.Desktop.MVP.Forms;
using JToolbox.WinForms.MVP;
using System.Threading.Tasks;

namespace Examples.Desktop.MVP.Presenters
{
    public class ResultPresenter : Presenter<IResultView>
    {
        private string inputString;

        public ResultPresenter(PresenterFactory presenterFactory)
            : base(presenterFactory)
        {
        }

        protected override Task OnAttach()
        {
            View.OnAccept += View_OnAccept;
            View.OnCancel += View_OnCancel;
            return base.OnAttach();
        }

        protected override Task OnInitialize()
        {
            inputString = (string)Input;
            View.Value = inputString;
            return base.OnInitialize();
        }

        protected override Task OnDetach()
        {
            View.OnAccept -= View_OnAccept;
            View.OnCancel -= View_OnCancel;
            return base.OnDetach();
        }

        private void View_OnCancel()
        {
            Close(null);
        }

        private void View_OnAccept()
        {
            Close(inputString);
        }
    }
}