using Examples.Desktop.Base.ViewModels;
using System.Windows;

namespace Examples.Desktop.Base.Views
{
    /// <summary>
    /// Interaction logic for PrismWindow1.xaml
    /// </summary>
    public partial class SelectWindow : Window
    {
        public SelectWindow(SelectViewModel selectViewModel)
        {
            DataContext = selectViewModel;
            selectViewModel.Close += () => Close();
            InitializeComponent();
        }
    }
}