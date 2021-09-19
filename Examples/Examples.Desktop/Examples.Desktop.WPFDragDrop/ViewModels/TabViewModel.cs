using JToolbox.WPF.Core.Awareness;
using JToolbox.WPF.Core.Awareness.Args;
using JToolbox.WPF.Core.Base;
using System.Collections.ObjectModel;

namespace Examples.Desktop.WPFDragDrop.ViewModels
{
    public class TabViewModel : BaseViewModel, IDragDropAware
    {
        private ObservableCollection<ItemViewModel> items;
        private string name;

        public ObservableCollection<ItemViewModel> Items
        {
            get => items;
            set => Set(ref items, value);
        }

        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public void OnDrag(DragDropArgs args)
        {
            EventLogs.AddWithClassName("Tab OnDrag");
        }

        public void OnDrop(DragDropArgs args)
        {
            EventLogs.AddWithClassName("Tab OnDrop, source: " + args.Source.GetType().Name);
        }

        public void ReplaceItem(ItemViewModel item, ItemViewModel other)
        {
            var source = Items.IndexOf(item);
            var target = Items.IndexOf(other);
            Items.Move(source, target);
        }

        public void SetAsLast(ItemViewModel item)
        {
            Items.Remove(item);
            Items.Add(item);
        }
    }
}