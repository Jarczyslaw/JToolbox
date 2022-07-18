using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using Prism.Commands;
using Prism.Navigation;
using System;
using Xamarin.Essentials;

namespace XamarinPrismApp.ViewModels
{
    public class SecureLocalStorageViewModel : ViewModelBase
    {
        private readonly IDialogsService dialogsService;
        private readonly string key = "key";
        private string text;

        public SecureLocalStorageViewModel(INavigationService navigationService, IDialogsService dialogsService)
            : base(navigationService)
        {
            Title = "Secure local storage";

            this.dialogsService = dialogsService;

            Load();
        }

        public DelegateCommand LoadCommand => new DelegateCommand(Load);

        public DelegateCommand SaveCommand => new DelegateCommand(Save);

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        private async void Load()
        {
            try
            {
                Text = await SecureStorage.GetAsync(key);
            }
            catch (Exception exc)
            {
                await dialogsService.Error(exc, null);
            }
        }

        private async void Save()
        {
            try
            {
                await SecureStorage.SetAsync(key, text);
                dialogsService.Toast("Saved");
            }
            catch (Exception exc)
            {
                await dialogsService.Error(exc, null);
            }
        }
    }
}