using Examples.Desktop.Base.ViewModels;
using System.Windows;

namespace Examples.Desktop.Base.Views
{
    /// <summary>
    /// Interaction logic for PrismWindow1.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public InputWindow(InputViewModel inputViewModel)
        {
            DataContext = inputViewModel;
            inputViewModel.Close += () => Close();
            InitializeComponent();
        }
    }
}