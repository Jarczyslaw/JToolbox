using JToolbox.XamarinForms.Core.Base;
using Prism.Commands;
using Prism.Navigation;

namespace XamarinPrismApp.ViewModels
{
    public class SubnaviViewModel : ViewModelBase
    {
        public SubnaviViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Subnavigation";
        }

        public DelegateCommand ReturnCommand => new DelegateCommand(async () => await Close());
        public DelegateCommand ReturnToRootCommand => new DelegateCommand(async () => await ReturnToRoot());
    }
}