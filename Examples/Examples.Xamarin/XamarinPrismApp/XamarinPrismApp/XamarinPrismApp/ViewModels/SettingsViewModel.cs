using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using Prism.Commands;
using Prism.Navigation;
using XamarinPrismApp.Services;

namespace XamarinPrismApp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string setValue, currentValue;
        private readonly IAppSettings appSettings;
        private readonly IDialogsService dialogsService;

        public SettingsViewModel(IAppSettings appSettings, IDialogsService dialogsService, INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Settings";

            this.appSettings = appSettings;
            this.dialogsService = dialogsService;

            CurrentValue = appSettings.Value;
        }

        public string SetValue
        {
            get => setValue;
            set => SetProperty(ref setValue, value);
        }

        public string CurrentValue
        {
            get => currentValue;
            set => SetProperty(ref currentValue, value);
        }

        public DelegateCommand SetCommand => new DelegateCommand(() =>
        {
            if (string.IsNullOrEmpty(setValue))
            {
                dialogsService.Toast("Value can not be empty");
                return;
            }

            appSettings.Value = setValue;
            CurrentValue = appSettings.Value;
        });

        public DelegateCommand ClearCommand => new DelegateCommand(() =>
        {
            appSettings.Clear();
            CurrentValue = appSettings.Value;
        });
    }
}