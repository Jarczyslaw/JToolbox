﻿using JToolbox.WPF.Core.Awareness;
using JToolbox.WPF.Core.Awareness.Args;
using JToolbox.WPF.Core.Base;
using System.Collections.ObjectModel;

namespace Examples.Desktop.WPFDragDrop.ViewModels
{
    public class TabsContextViewModel : BaseViewModel, IDragDropAware
    {
        private TabViewModel selectedTab;
        private ObservableCollection<TabViewModel> tabs;

        public TabsContextViewModel()
        {
            tabs = new ObservableCollection<TabViewModel>
            {
                new TabViewModel
                {
                    Name = "Tab1",
                    Items = new ObservableCollection<ItemViewModel>
                    {
                        new ItemViewModel
                        {
                            Name = "Item1"
                        },
                        new ItemViewModel
                        {
                            Name = "Item2"
                        },
                        new ItemViewModel
                        {
                            Name = "Item3"
                        }
                    }
                },
                new TabViewModel
                {
                    Name = "Tab2",
                    Items = new ObservableCollection<ItemViewModel>
                    {
                        new ItemViewModel
                        {
                            Name = "Item4"
                        },
                        new ItemViewModel
                        {
                            Name = "Item5"
                        },
                        new ItemViewModel
                        {
                            Name = "Item6"
                        },
                        new ItemViewModel
                        {
                            Name = "Item7"
                        }
                    }
                },
                new TabViewModel
                {
                    Name = "Tab3",
                    Items = new ObservableCollection<ItemViewModel>
                    {
                        new ItemViewModel
                        {
                            Name = "Item8"
                        },
                        new ItemViewModel
                        {
                            Name = "Item9"
                        }
                    }
                }
            };
            SelectedTab = Tabs[0];
        }

        public TabViewModel SelectedTab
        {
            get => selectedTab;
            set => Set(ref selectedTab, value);
        }

        public ObservableCollection<TabViewModel> Tabs
        {
            get => tabs;
            set => Set(ref tabs, value);
        }

        public void OnDrag(DragDropArgs args)
        {
            EventLogs.AddWithClassName("OnDrag, source: " + args.Source.GetType().Name);
        }

        public void OnDrop(DragDropArgs args)
        {
            var source = args.Source;
            var target = args.Target;
            EventLogs.AddWithClassName($"OnDrop source: {source.GetType().Name}, target {target.GetType().Name}");
            if (source is TabViewModel sourceTab)
            {
                if (target is TabViewModel targetTab)
                {
                    MoveTab(sourceTab, targetTab);
                }
                else if (target is TabsContextViewModel)
                {
                    SetTabAsLast(sourceTab);
                }
            }
            else if (source is ItemViewModel sourceItem)
            {
                if (target is ItemViewModel targetItem)
                {
                    var tab = GetTabByItem(sourceItem);
                    MoveItem(tab, sourceItem, targetItem);
                }
                else if (target is TabViewModel targetTab)
                {
                    var tab = GetTabByItem(sourceItem);
                    if (tab == targetTab)
                    {
                        SetItemAsLast(tab, sourceItem);
                    }
                    else
                    {
                        MoveFromTabToTab(tab, sourceItem, targetTab);
                        SelectedTab = tab;
                    }
                }
            }
        }

        private TabViewModel GetTabByItem(ItemViewModel item)
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

        private void MoveFromTabToTab(TabViewModel sourceTab, ItemViewModel item, TabViewModel targetTab)
        {
            sourceTab.Items.Remove(item);
            targetTab.Items.Add(item);
            SelectedTab = targetTab;
        }

        private void MoveItem(TabViewModel tab, ItemViewModel source, ItemViewModel target)
        {
            var items = tab.Items;
            items.Move(items.IndexOf(source), items.IndexOf(target));
        }

        private void MoveTab(TabViewModel source, TabViewModel target)
        {
            Tabs.Move(Tabs.IndexOf(source), Tabs.IndexOf(target));
            SelectedTab = source;
        }

        private void SetItemAsLast(TabViewModel tab, ItemViewModel item)
        {
            var items = tab.Items;
            items.Remove(item);
            items.Add(item);
        }

        private void SetTabAsLast(TabViewModel tab)
        {
            Tabs.Remove(tab);
            Tabs.Add(tab);
            SelectedTab = tab;
        }
    }
}