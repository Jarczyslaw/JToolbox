using JToolbox.WPF.Core.Base;
using System.Collections.ObjectModel;

namespace WpfMvvmDragDrop.ViewModels
{
    public class TabViewModel : BaseViewModel
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
    }
}