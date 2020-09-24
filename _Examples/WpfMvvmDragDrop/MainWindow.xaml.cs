using JToolbox.WPF.UI.DragAndDrop;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WpfMvvmDragDrop.ViewModels;

namespace WpfMvvmDragDrop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbLogs.TextChanged += TbLogs_TextChanged;

            DataContext = new MainViewModel();
            new DragDropHelper(tabControl, new List<DragDropPair>
            {
                new DragDropPair(typeof(ListViewItem)),
                new DragDropPair(typeof(ListViewItem),typeof(TabItem)),
                new DragDropPair(typeof(ListViewItem), typeof(ListView)),
                new DragDropPair(typeof(TabItem)),
                new DragDropPair(typeof(TabItem), typeof(TabPanel))
            });

            new FileDragDropHelper(listView, new List<Type> { typeof(ListViewItem) }, new List<Type> { typeof(ListView) });
        }

        private void TbLogs_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbLogs.ScrollToEnd();
        }
    }
}