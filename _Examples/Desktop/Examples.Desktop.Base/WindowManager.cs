using Examples.Desktop.Base.ViewModels;
using Examples.Desktop.Base.Views;

namespace Examples.Desktop.Base
{
    public static class WindowManager
    {
        public static MainWindow GetMainWindow(string title)
        {
            var viewModel = new MainViewModel
            {
                Title = title
            };
            return new MainWindow(viewModel);
        }

        public static string GetInput(string label, string text = null)
        {
            var viewModel = new InputViewModel
            {
                Text = text,
                Label = label
            };
            var window = new InputWindow(viewModel);
            window.ShowDialog();
            return viewModel.Result;
        }
    }
}