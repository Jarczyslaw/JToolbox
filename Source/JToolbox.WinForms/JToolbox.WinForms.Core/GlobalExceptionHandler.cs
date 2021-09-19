using System;
using System.Threading;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core
{
    public abstract class GlobalExceptionHandler
    {
        public void Attach()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        protected abstract void HandleException(Exception exception, string source);

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception, nameof(Application_ThreadException));
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException((Exception)e.ExceptionObject, nameof(CurrentDomain_UnhandledException));
        }
    }
}