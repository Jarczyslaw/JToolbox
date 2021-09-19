using Examples.Desktop.WPFDragDrop.ViewModels;
using JToolbox.WPF.UI.DragAndDrop;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Examples.Desktop.WPFDragDrop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbLogs.TextChanged += TbLogs_TextChanged;

            DataContext = new MainViewModel();
            var dragDropHelper = new DragDropHelper(tabControl, new List<DragDropPair>
            {
                new DragDropPair(typeof(ListViewItem)),
                new DragDropPair(typeof(ListViewItem),typeof(TabItem)),
                new DragDropPair(typeof(ListViewItem), typeof(ListView)),
                new DragDropPair(typeof(TabItem)),
                new DragDropPair(typeof(TabItem), typeof(TabPanel))
            });
            dragDropHelper.OnDrag += DragDropHelper_OnDrag;
            dragDropHelper.OnDrop += DragDropHelper_OnDrop;

            var fileDragDropHelper = new FileDragDropHelper(listView, new List<Type> { typeof(ListViewItem) }, new List<Type> { typeof(ListView) });
            fileDragDropHelper.OnFileDrag += FileDragDropHelper_OnFileDrag;
            fileDragDropHelper.OnFileDrop += FileDragDropHelper_OnFileDrop;
        }

        private void DragDropHelper_OnDrag(UiDragDropArgs args)
        {
            EventLogs.AddWithClassName("OnDrag, source: " + args.SourceElement.GetType().Name);
        }

        private void DragDropHelper_OnDrop(UiDragDropArgs args)
        {
            EventLogs.AddWithClassName("OnDrop, source: " + args.SourceElement.GetType().Name + ", target: " + args.TargetElement.GetType().Name);
        }

        private void FileDragDropHelper_OnFileDrag(UiFileDragArgs args)
        {
            EventLogs.AddWithClassName("OnFileDrag, element: " + args.Element.GetType().Name);
        }

        private void FileDragDropHelper_OnFileDrop(UiFileDropArgs args)
        {
            EventLogs.AddWithClassName("OnFileDrop, element: " + args.Element.GetType().Name);
        }

        private void TbLogs_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbLogs.ScrollToEnd();
        }
    }
}