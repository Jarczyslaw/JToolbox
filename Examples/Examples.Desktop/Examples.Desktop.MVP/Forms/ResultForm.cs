using JToolbox.WinForms.MVP;
using System;

namespace Examples.Desktop.MVP.Forms
{
    public partial class ResultForm : FormView, IResultView
    {
        public ResultForm()
        {
            InitializeComponent();
        }

        public event Accept OnAccept;

        public event Cancel OnCancel;

        public string Value
        {
            set
            {
                lblValue.Text = "Passed value: " + value;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            OnAccept();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancel();
        }
    }
}