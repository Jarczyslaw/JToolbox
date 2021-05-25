using System.Threading.Tasks;

namespace JToolbox.WinForms.MVP
{
    public delegate Task ViewShown();

    public delegate Task ViewLoaded();

    public delegate Task<bool> ViewClosing();

    public delegate Task ViewClosed();

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