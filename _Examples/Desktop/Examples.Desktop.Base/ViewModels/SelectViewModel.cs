using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Examples.Desktop.Base.ViewModels
{
    public class SelectViewModel : BindableBase
    {
        private string label;
        private SelectItemViewModel selectedItem;

        public SelectViewModel(List<object> items)
        {
            Items = new ObservableCollection<SelectItemViewModel>(items.Select(i => new SelectItemViewModel
            {
                Value = i
            }));
            SelectedItem = Items[0];
        }

        public DelegateCommand SaveCommand => new DelegateCommand(() =>
        {
            Result = SelectedItem.Value;
            Close?.Invoke();
        });

        public DelegateCommand CloseCommand => new DelegateCommand(() => Close?.Invoke());

        public SelectItemViewModel SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public ObservableCollection<SelectItemViewModel> Items;

        public string Label
        {
            get => label;
            set => SetProperty(ref label, value);
        }

        public Action Close { get; set; }

        public object Result { get; set; }
    }
}