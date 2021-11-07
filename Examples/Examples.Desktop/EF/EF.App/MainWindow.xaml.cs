using EF.DataAccess;
using System.Linq;
using System.Windows;

namespace EF.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var ctx = new Context())
            {
                ctx.Students.ToList();
            }
        }
    }
}