using System;

namespace JToolbox.WinForms.MVP
{
    public delegate void ViewClosed();

    public delegate bool ViewClosing();

    public delegate void ViewLoaded();

    public delegate void ViewShown();

    public interface IView
    {
        event ViewClosed OnViewClosed;

        event ViewClosing OnViewClosing;

        event ViewLoaded OnViewLoaded;

        event ViewShown OnViewShown;

        bool ViewEnabled { get; set; }
        string ViewTitle { set; }

        void CloseView();

        void ShowView();

        void ShowViewAsModal();

        void ViewBeginInvoke(Action action);

        void ViewInvoke(Action action);

        T ViewInvoke<T>(Func<T> func);
    }
}