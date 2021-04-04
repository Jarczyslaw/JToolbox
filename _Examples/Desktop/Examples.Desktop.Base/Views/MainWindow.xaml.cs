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
            if (!cleanedUp)
            {
                e.Cancel = true;
                var viewModel = DataContext as MainViewModel;
                Task.Run(async () =>
                {
                    await viewModel.CleanUp();
                    cleanedUp = true;
                    Threading.SafeInvoke(Close);
                });
            }
        }
    }
}