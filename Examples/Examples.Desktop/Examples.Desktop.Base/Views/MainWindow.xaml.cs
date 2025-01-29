using Examples.Desktop.Base.ViewModels;
using JToolbox.WPF.Core;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Examples.Desktop.Base.Views
{
    /// <summary>
    /// Interaction logic for PrismWindow1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool cleanedUp;

        public MainWindow(MainViewModel mainViewModel)
        {
            DataContext = mainViewModel;
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            (sender as TextBox).ScrollToEnd();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            if (!cleanedUp)
            {
                e.Cancel = true;
                if (!viewModel.CheckClose())
                {
                    return;
                }

                Task.Run(async () =>
                {
                    await viewModel.CleanUp();
                    cleanedUp = true;
                    Threading.SafeInvoke(Close);
                });
            }
        }

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.OnViewLoaded();
            }
        }
    }
}