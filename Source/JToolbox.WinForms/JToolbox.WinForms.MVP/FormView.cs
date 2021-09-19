using JToolbox.WinForms.Core.Extensions;
using System;
using System.Windows.Forms;

namespace JToolbox.WinForms.MVP
{
    public class FormView : Form, IView
    {
        public event ViewClosed OnViewClosed;

        public event ViewClosing OnViewClosing;

        public event ViewLoaded OnViewLoaded;

        public event ViewShown OnViewShown;

        public bool ViewEnabled
        {
            get => Enabled;
            set => this.SafeInvoke(() => Enabled = value);
        }

        public string ViewTitle
        {
            set => this.SafeInvoke(() => Text = value);
        }

        public virtual void CloseView()
        {
            this.SafeInvoke(() => Close());
        }

        public virtual void ShowView()
        {
            this.SafeInvoke(() => Show());
        }

        public virtual void ShowViewAsModal()
        {
            this.SafeInvoke(() => ShowDialog());
        }

        public void ViewBeginInvoke(Action action)
        {
            this.SafeBeginInvoke(action);
        }

        public void ViewInvoke(Action action)
        {
            this.SafeInvoke(action);
        }

        public T ViewInvoke<T>(Func<T> func)
        {
            return this.SafeInvoke<T>(func);
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

        protected override void OnLoad(EventArgs e)
        {
            OnViewLoaded?.Invoke();
            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            OnViewShown?.Invoke();
        }
    }
}