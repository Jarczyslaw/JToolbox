using JToolbox.WPF.UI.DragAndDrop;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WpfMvvmDragDrop.ViewModels;

namespace WpfMvvmDragDrop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DragDropHelper dragDrop;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Context();
            dragDrop = new DragDropHelper(tabControl, "test", new List<DragDropPair>
            {
                new DragDropPair
                {
                    SourceType = typeof(ListViewItem),
                    TargetType = typeof(ListViewItem)
                },
                new DragDropPair
                {
                    SourceType = typeof(ListViewItem),
                    TargetType = typeof(TabItem)
                },
                new DragDropPair
                {
                    SourceType = typeof(ListViewItem),
                    TargetType = typeof(ListView)
                },
                new DragDropPair
                {
                    SourceType = typeof(TabItem),
                    TargetType = typeof(TabItem)
                },
                new DragDropPair
                {
                    SourceType = typeof(TabItem),
                    TargetType = typeof(TabPanel)
                }
            });
        }
    }
}