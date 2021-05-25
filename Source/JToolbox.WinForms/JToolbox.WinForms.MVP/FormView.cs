using JToolbox.WinForms.Core.Extensions;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JToolbox.WinForms.MVP
{
    public class FormView : Form, IView
    {
        private bool skipClosing;

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

        protected override async void OnFormClosed(FormClosedEventArgs e)
        {
            if (OnViewClosed != null)
            {
                await OnViewClosed();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!skipClosing && OnViewClosing != null)
            {
                Enabled = false;
                e.Cancel = true;
                Task.Run(async () =>
                {
                    var cancel = await OnViewClosing();
                    if (!cancel)
                    {
                        skipClosing = true;
                        this.SafeInvoke(Close);
                    }
                    else
                    {
                        this.SafeInvoke(() => Enabled = true);
                    }
                });
            }
        }

        protected override async void OnShown(EventArgs e)
        {
            if (OnViewShown != null)
            {
                await OnViewShown();
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            if (OnViewLoaded != null)
            {
                await OnViewLoaded();
            }
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