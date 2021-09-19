using JToolbox.XamarinForms.Core.Base;
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (Parameters != null)
            {
                Input = "Input: " + (string)Parameters.Value;
                SourceViewModel = "Source view model: " + Parameters.SourceViewModel.GetType().Name;
            }
        }
    }
}