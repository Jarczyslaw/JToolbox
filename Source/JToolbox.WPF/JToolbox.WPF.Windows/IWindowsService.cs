using System.Windows;

namespace Barcodes.Services.Windows
{
    public interface IWindowsService
    {
        Window GetActiveWindow();

        bool IsWindowOpen<T>() where T : Window;

        void RestoreWindows<T>() where T : Window;

        void Show(Window window, object dataContext = null, Window owner = null);

        bool? ShowDialog(Window window, object dataContext = null);
    }
}