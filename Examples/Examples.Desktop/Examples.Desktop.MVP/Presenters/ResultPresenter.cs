using Examples.Desktop.MVP.Forms;
using JToolbox.WinForms.MVP;

namespace Examples.Desktop.MVP.Presenters
{
    public class ResultPresenter : Presenter<IResultView>
    {
        private string inputString;

        public ResultPresenter(PresenterFactory presenterFactory)
            : base(presenterFactory)
        {
        }

        public override void Attach(IResultView view)
        {
            base.Attach(view);
            View.OnAccept += View_OnAccept;
            View.OnCancel += View_OnCancel;
        }

        public override void Initialize(object input)
        {
            base.Initialize(input);
            inputString = (string)input;
            View.Value = inputString;
        }

        protected override void Detach()
        {
            View.OnAccept -= View_OnAccept;
            View.OnCancel -= View_OnCancel;
        }

        private void View_OnAccept()
        {
            Close(inputString);
        }

        private void View_OnCancel()
        {
            Close(null);
        }
    }
}