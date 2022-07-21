using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation;
using Prism.Navigation;

namespace XamarinPrismApp.ViewModels
{
    public class NaviInputViewModel : ViewModelBase
    {
        private string sourceViewModel, input;

        public NaviInputViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Navigation input";
        }

        public string Input
        {
            get => input;
            set => SetProperty(ref input, value);
        }

        public string SourceViewModel
        {
            get => sourceViewModel;
            set => SetProperty(ref sourceViewModel, value);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            var @params = NavigationParams<string, object>.CreateFromNavigationParameters(parameters);
            Input = "Input: " + @params.Argument;
            SourceViewModel = "Source view model: " + @params.SourceViewModel.GetType().Name;
        }
    }
}