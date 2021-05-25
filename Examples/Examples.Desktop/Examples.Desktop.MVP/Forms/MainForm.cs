using JToolbox.WinForms.MVP;
using System.Windows.Forms;

namespace Examples.Desktop.MVP.Forms
{
    public partial class MainForm : FormView, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public event Pass OnPass;

        private void btnPass_Click(object sender, System.EventArgs e)
        {
            OnPass?.Invoke(false, tbValue.Text);
        }

        private void btnPassModal_Click(object sender, System.EventArgs e)
        {
            OnPass?.Invoke(true, tbValue.Text);
        }

        public override void ShowView()
        {
            Application.Run(this);
        }
    }
}