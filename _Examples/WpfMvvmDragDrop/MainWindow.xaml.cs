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
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            new DragDropHelper(tabControl, new List<DragDropPair>
            {
                new DragDropPair(typeof(ListViewItem)),
                new DragDropPair(typeof(ListViewItem),typeof(TabItem)),
                new DragDropPair(typeof(ListViewItem), typeof(ListView)),
                new DragDropPair(typeof(TabItem)),
                new DragDropPair(typeof(TabItem), typeof(TabPanel))
            });

            new FileDragDropHelper(listView);
        }
    }
}