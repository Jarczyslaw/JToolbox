using JToolbox.XamarinForms.Core.Base;
using Prism.Commands;
using Prism.Navigation;
using XamarinPrismApp.Views;
using JToolbox.XamarinForms.Core.Navigation;

namespace XamarinPrismApp.ViewModels
{
    public class NaviViewModel : ViewModelBase
    {
        private string message;
        private bool messageVisible;
        private string inputValue;

        public NaviViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Navigation";
        }

        public DelegateCommand NaviSubCommand => new DelegateCommand(async () => await Navigate<SubnaviViewModel>());

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

        public DelegateCommand NavigateWithInputCommand => new DelegateCommand(async () =>
        {
            var @params = new Parameters
            {
                SourceViewModel = this,
                Value = InputValue
            };
            await Navigate<NaviInputViewModel>(@params);
        });

        public string InputValue
        {
            get => inputValue;
            set => SetProperty(ref inputValue, value);
        }

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

        private void SetResult(string functionName, object result)
        {
            var res = result == null ? "null" : result.ToString();
            Message = $"{functionName} result: {res}";
        }
    }
}