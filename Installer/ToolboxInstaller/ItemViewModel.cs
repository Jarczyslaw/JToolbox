using JToolbox.WPF.Core.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ToolboxInstaller
{
    public class ItemViewModel : BaseViewModel
    {
        private bool isChecked;
        private ObservableCollection<ItemViewModel> children = new ObservableCollection<ItemViewModel>();
        private bool isProject;
        private string title;
        private bool isExpanded = true;
        private ItemViewModel parent;

        public Visibility ProjectVisibility => IsProject ? Visibility.Visible : Visibility.Collapsed;

        public string SourcePath { get; set; }

        public ItemViewModel Parent
        {
            get => parent;
            set => Set(ref parent, value);
        }

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public bool IsChecked
        {
            get => isChecked;
            set => SetChecked(value, true);
        }

        public bool IsExpanded
        {
            get => isExpanded;
            set => Set(ref isExpanded, value);
        }

        public bool IsProject
        {
            get => isProject;
            set => Set(ref isProject, value);
        }

        public ObservableCollection<ItemViewModel> Children
        {
            get => children;
            set => Set(ref children, value);
        }

        public ItemViewModel AddChild(ItemViewModel item)
        {
            item.Parent = this;
            Children.Add(item);
            return this;
        }

        public ItemViewModel AddChildren(IEnumerable<ItemViewModel> items)
        {
            foreach (var item in items)
            {
                AddChild(item);
            }
            return this;
        }

        public bool HasCheckedChildren => children.Any(s => s.IsChecked);

        private void UpdateParent(bool value, ItemViewModel parent)
        {
            if (parent != null)
            {
                parent.SetChecked(parent.HasCheckedChildren, false);
                UpdateParent(value, parent.Parent);
            }
        }

        private void UpdateChildren(bool value, IEnumerable<ItemViewModel> children)
        {
            foreach (var child in children)
            {
                child.SetChecked(value, false);
                UpdateChildren(value, child.Children);
            }
        }

        public void SetChecked(bool value, bool update)
        {
            Set(ref isChecked, value, nameof(IsChecked));
            if (update)
            {
                UpdateParent(value, parent);
                UpdateChildren(value, children);
            }
        }
    }
}