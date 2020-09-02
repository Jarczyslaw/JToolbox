using JToolbox.WPF.Core.Awareness;
using JToolbox.WPF.Core.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfMvvmDragDrop.ViewModels
{
    public class Context : BaseViewModel, IDragDropAware, IFileDropAware
    {
        private ObservableCollection<Tab> tabs;
        private Tab selectedTab;

        public Context()
        {
            tabs = new ObservableCollection<Tab>
            {
                new Tab
                {
                    Name = "Tab1",
                    Items = new ObservableCollection<Item>
                    {
                        new Item
                        {
                            Name = "Item1"
                        },
                        new Item
                        {
                            Name = "Item2"
                        },
                        new Item
                        {
                            Name = "Item3"
                        }
                    }
                },
                new Tab
                {
                    Name = "Tab2",
                    Items = new ObservableCollection<Item>
                    {
                        new Item
                        {
                            Name = "Item4"
                        },
                        new Item
                        {
                            Name = "Item5"
                        },
                        new Item
                        {
                            Name = "Item6"
                        },
                        new Item
                        {
                            Name = "Item7"
                        }
                    }
                },
                new Tab
                {
                    Name = "Tab3",
                    Items = new ObservableCollection<Item>
                    {
                        new Item
                        {
                            Name = "Item8"
                        },
                        new Item
                        {
                            Name = "Item9"
                        }
                    }
                }
            };
            SelectedTab = Tabs[0];
        }

        public Tab SelectedTab
        {
            get => selectedTab;
            set => Set(ref selectedTab, value);
        }

        public ObservableCollection<Tab> Tabs
        {
            get => tabs;
            set => Set(ref tabs, value);
        }

        public void OnDragDrop(object source, object target)
        {
            if (source is Tab sourceTab)
            {
                if (target is Tab targetTab)
                {
                    MoveTab(sourceTab, targetTab);
                }
                else if (target is Context)
                {
                    SetTabAsLast(sourceTab);
                }
            }
            else if (source is Item sourceItem)
            {
                if (target is Item targetItem)
                {
                    var tab = GetTabByItem(sourceItem);
                    MoveItem(tab, sourceItem, targetItem);
                }
                else if (target is Tab targetTab)
                {
                    var tab = GetTabByItem(sourceItem);
                    if (tab == targetTab)
                    {
                        SetItemAsLast(tab, sourceItem);
                    }
                    else
                    {
                        MoveFromTabToTab(tab, sourceItem, targetTab);
                    }
                }
            }
        }

        private void MoveTab(Tab source, Tab target)
        {
            Tabs.Move(Tabs.IndexOf(source), Tabs.IndexOf(target));
            SelectedTab = source;
        }

        private void MoveItem(Tab tab, Item source, Item target)
        {
            var items = tab.Items;
            items.Move(items.IndexOf(source), items.IndexOf(target));
        }

        private void SetItemAsLast(Tab tab, Item item)
        {
            var items = tab.Items;
            items.Remove(item);
            items.Add(item);
        }

        private void SetTabAsLast(Tab tab)
        {
            Tabs.Remove(tab);
            Tabs.Add(tab);
            SelectedTab = tab;
        }

        private void MoveFromTabToTab(Tab sourceTab, Item item, Tab targetTab)
        {
            sourceTab.Items.Remove(item);
            targetTab.Items.Add(item);
            SelectedTab = targetTab;
        }

        private Tab GetTabByItem(Item item)
        {
            foreach (var tab in Tabs)
            {
                if (tab.Items.Contains(item))
                {
                    return tab;
                }
            }
            return null;
        }

        public void OnFileDrop(List<string> filePaths)
        {
            if (SelectedTab != null)
            {
                foreach (var file in filePaths)
                {
                    SelectedTab.Items.Add(new Item
                    {
                        Name = Path.GetFileNameWithoutExtension(file)
                    });
                }
            }
        }
    }
}