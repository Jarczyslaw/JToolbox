using Prism.Mvvm;
using XamarinPrismApp.Models;

namespace XamarinPrismApp.ViewModels
{
    public class DialogsEntryViewModel : BindableBase
    {
        private DialogsEntryType type;
        private string title;

        public DialogsEntryViewModel(DialogsEntryType dialogsEntryType)
        {
            Type = dialogsEntryType;
            Title = Type.ToString();
        }

        public DialogsEntryType Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
    }
}