using Prism.Mvvm;
using XamarinPrismApp.Models;

namespace XamarinPrismApp.ViewModels
{
    public class DialogsEntryViewModel : BindableBase
    {
        private string title;
        private DialogsEntryType type;

        public DialogsEntryViewModel(DialogsEntryType dialogsEntryType)
        {
            Type = dialogsEntryType;
            Title = Type.ToString();
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public DialogsEntryType Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }
    }
}