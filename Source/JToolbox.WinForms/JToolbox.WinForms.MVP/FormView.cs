using JToolbox.WinForms.Core.Extensions;
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
            set => this.SafeInvoke(() => Text = value);
        }

        public bool ViewEnabled
        {
            get => Enabled;
            set => this.SafeInvoke(() => Enabled = value);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            OnViewClosed?.Invoke();
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
            OnViewShown?.Invoke();
        }

        protected override void OnLoad(EventArgs e)
        {
            OnViewLoaded?.Invoke();
            base.OnLoad(e);
        }

        public virtual void ShowView()
        {
            this.SafeInvoke(() => Show());
        }

        public virtual void ShowViewAsModal()
        {
            this.SafeInvoke(() => ShowDialog());
        }

        public virtual void CloseView()
        {
            this.SafeInvoke(() => Close());
        }

        public void ViewInvoke(Action action)
        {
            this.SafeInvoke(action);
        }

        public void ViewBeginInvoke(Action action)
        {
            this.SafeBeginInvoke(action);
        }
    }
}