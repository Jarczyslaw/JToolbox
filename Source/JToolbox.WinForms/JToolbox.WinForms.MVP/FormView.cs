using System;
using System.Windows.Forms;

namespace JToolbox.WinForms.MVP
{
    public class FormView : Form, IView
    {
        public event ViewShown OnViewShown;

        public event ViewLoaded OnViewLoaded;

        public event ViewClosing OnViewClosing;

        public event ViewClosed OnViewClosed;

        public string ViewTitle
        {
            set
            {
                Text = value;
            }
        }

        public bool ViewEnabled
        {
            get => Enabled;
            set => Enabled = value;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (OnViewClosed != null)
            {
                OnViewClosed();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (OnViewClosing != null)
            {
                e.Cancel = OnViewClosing();
            }
            base.OnClosing(e);
        }

        protected override void OnShown(EventArgs e)
        {
            if (OnViewShown != null)
            {
                OnViewShown();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (OnViewLoaded != null)
            {
                OnViewLoaded();
            }
            base.OnLoad(e);
        }

        public virtual void ShowView()
        {
            Show();
        }

        public virtual void ShowViewAsModal()
        {
            ShowDialog();
        }

        public virtual void CloseView()
        {
            Close();
        }
    }
}