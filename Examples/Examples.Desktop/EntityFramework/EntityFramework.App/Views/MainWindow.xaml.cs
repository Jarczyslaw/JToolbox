using EntityFramework.App.ViewModels;
using System.Windows;
using Unity;

namespace EntityFramework.App.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(IUnityContainer container)
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            DataContext = container.Resolve<MainViewModel>();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tcTabs.SelectedItem = tiStudents;
        }
    }
}