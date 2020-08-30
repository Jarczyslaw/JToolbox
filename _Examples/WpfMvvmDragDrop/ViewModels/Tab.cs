using JToolbox.WPF.Core.Base;
using System.Collections.ObjectModel;

namespace WpfMvvmDragDrop.ViewModels
{
    public class Tab : BaseViewModel
    {
        private string name;
        private ObservableCollection<Item> items;

        public ObservableCollection<Item> Items
        {
            get => items;
            set => Set(ref items, value);
        }

        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public void SetAsLast(Item item)
        {
            Items.Remove(item);
            Items.Add(item);
        }

        public void ReplaceItem(Item item, Item other)
        {
            var source = Items.IndexOf(item);
            var target = Items.IndexOf(other);
            Items.Move(source, target);
        }
    }
}