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

            DataContext = container.Resolve<MainViewModel>();
        }
    }
}