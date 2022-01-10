using EntityFramework.BusinessLogic;
using JToolbox.Desktop.Dialogs;
using JToolbox.WPF.Core.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EntityFramework.App.ViewModels.Common
{
    public abstract class CommonListViewModel<T> : BaseViewModel, IOnRefresh
    {
        protected readonly IBusinessService business;
        protected readonly IDialogsService dialogs;
        private RelayCommand addCommand;
        private RelayCommand editCommand;
        private ObservableCollection<T> items;
        private RelayCommand removeCommand;
        private T selectedItem;

        protected CommonListViewModel(IBusinessService business, IDialogsService dialogs)
        {
            this.dialogs = dialogs;
            this.business = business;
        }

        public RelayCommand AddCommand => addCommand ?? (addCommand = new RelayCommand(() => AddItem()));

        public RelayCommand EditCommand => editCommand
            ?? (editCommand = new RelayCommand(() => EditItem(SelectedItem), () => SelectedItem != null));

        public ObservableCollection<T> Items
        {
            get => items;
            set => Set(ref items, value);
        }

        public RelayCommand RemoveCommand => removeCommand
            ?? (removeCommand = new RelayCommand(() => RemoveItem(SelectedItem), () => SelectedItem != null));

        public T SelectedItem
        {
            get => selectedItem;
            set
            {
                Set(ref selectedItem, value);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public virtual void OnRefresh()
        {
            LoadItems();
        }

        protected abstract void AddItem();

        protected abstract void EditItem(T item);

        protected abstract IEnumerable<T> GetItems();

        protected void LoadItems()
        {
            try
            {
                Items = new ObservableCollection<T>(GetItems());
            }
            catch (Exception exc)
            {
                dialogs.ShowException(exc);
            }
        }

        protected abstract void RemoveItem(T item);
    }
}