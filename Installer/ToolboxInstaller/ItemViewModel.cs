using JToolbox.WPF.Core.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace ToolboxInstaller
{
    public class ItemViewModel : BaseViewModel
    {
        private ObservableCollection<ItemViewModel> children = new ObservableCollection<ItemViewModel>();
        private bool isChecked;
        private bool isExpanded = true;
        private bool isProject;
        private ItemViewModel parent;
        private string title;

        public ObservableCollection<ItemViewModel> Children
        {
            get => children;
            set => Set(ref children, value);
        }

        public bool HasCheckedChildren => children.Any(s => s.IsChecked);

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

        public ItemViewModel Parent
        {
            get => parent;
            set => Set(ref parent, value);
        }

        public Visibility ProjectVisibility => IsProject ? Visibility.Visible : Visibility.Collapsed;

        public string SourcePath { get; set; }

        public string Title
        {
            get => title;
            set => Set(ref title, value);
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

        public string GetPath()
        {
            var result = string.Empty;
            var currentParent = parent;
            while (currentParent != null)
            {
                result = Path.Combine(currentParent.Title, result);
                currentParent = currentParent.Parent;
            }
            return result;
        }

        public void SetChecked(bool value, bool update)
        {
            Set(ref isChecked, value, nameof(IsChecked));
            if (update)
            {
                UpdateChildren(value, children);
                UpdateParents(parent);
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

        private void UpdateParents(ItemViewModel parent)
        {
            if (parent != null)
            {
                parent.SetChecked(parent.HasCheckedChildren, false);
                UpdateParents(parent.Parent);
            }
        }
    }
}