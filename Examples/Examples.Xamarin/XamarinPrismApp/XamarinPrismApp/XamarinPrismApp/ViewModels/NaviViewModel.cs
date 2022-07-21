using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation;
using Prism.Commands;
using Prism.Navigation;
using XamarinPrismApp.Views;

namespace XamarinPrismApp.ViewModels
{
    public class NaviViewModel : ViewModelBase
    {
        private string inputValue;
        private string message;
        private bool messageVisible;

        public NaviViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Navigation";
        }

        public string InputValue
        {
            get => inputValue;
            set => SetProperty(ref inputValue, value);
        }

        public DelegateCommand IsCurrentPageCommand => new DelegateCommand(() =>
        {
            var result = navigationService.IsCurrentPage<NaviPage>();
            SetResult(nameof(IsCurrentPageCommand), result);
        });

        public DelegateCommand IsCurrentViewModelCommand => new DelegateCommand(() =>
        {
            var result = navigationService.IsCurrentViewModel<NaviViewModel>();
            SetResult(nameof(IsCurrentViewModelCommand), result);
        });

        public DelegateCommand IsViewModelOpenedCommand => new DelegateCommand(() =>
        {
            var result = navigationService.IsViewModelOpened<MainViewModel>();
            SetResult(nameof(IsViewModelOpenedCommand), result);
        });

        public string Message
        {
            get => message;
            set
            {
                SetProperty(ref message, value);
                MessageVisible = !string.IsNullOrEmpty(value);
            }
        }

        public bool MessageVisible
        {
            get => messageVisible;
            set => SetProperty(ref messageVisible, value);
        }

        public DelegateCommand NavigateWithInputCommand => new DelegateCommand(async () =>
        {
            var @params = new NavigationParams<string, object>
            {
                SourceViewModel = this,
                Argument = InputValue
            };
            await Navigate<NaviInputViewModel, string, object>(@params);
        });

        public DelegateCommand NaviSubCommand => new DelegateCommand(async () => await Navigate<SubnaviViewModel>());

        private void SetResult(string functionName, object result)
        {
            var res = result == null ? "null" : result.ToString();
            Message = $"{functionName} result: {res}";
        }
    }
}