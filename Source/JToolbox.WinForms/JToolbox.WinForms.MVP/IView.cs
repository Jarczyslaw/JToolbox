namespace JToolbox.WinForms.MVP
{
    public delegate void ViewShown();

    public delegate void ViewLoaded();

    public delegate bool ViewClosing();

    public delegate void ViewClosed();

    public interface IView
    {
        event ViewShown OnViewShown;

        event ViewLoaded OnViewLoaded;

        event ViewClosing OnViewClosing;

        event ViewClosed OnViewClosed;

        string ViewTitle { set; }
        bool ViewEnabled { get; set; }

        void ShowView();

        void ShowViewAsModal();

        void CloseView();
    }
}