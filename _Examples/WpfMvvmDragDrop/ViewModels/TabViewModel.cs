using JToolbox.WPF.Core.Awareness;
using JToolbox.WPF.Core.Base;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace WpfMvvmDragDrop.ViewModels
{
    public class TabViewModel : BaseViewModel, IDragDropAware
    {
        private string name;
        private ObservableCollection<ItemViewModel> items;

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

        public void SetAsLast(ItemViewModel item)
        {
            Items.Remove(item);
            Items.Add(item);
        }

        public void ReplaceItem(ItemViewModel item, ItemViewModel other)
        {
            var source = Items.IndexOf(item);
            var target = Items.IndexOf(other);
            Items.Move(source, target);
        }

        public void OnDrag(object source)
        {
            Debug.WriteLine("Tab OnDrag");
        }

        public void OnDrop(object source, object target)
        {
            Debug.WriteLine("Tab OnDrop, source: " + source.GetType().Name);
        }
    }
}