using JToolbox.XamarinForms.Core.Navigation;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;

namespace JToolbox.XamarinForms.Core.Base
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected readonly INavigationService navigationService;
        private string title;

        public ViewModelBase(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        protected Parameters Parameters { get; private set; }

        #region Navigation

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            Parameters = Parameters.CreateFromNavigationParameters(parameters);
        }

        protected Task<INavigationResult> Close(Parameters parameters = null)
        {
            return navigationService.Close(parameters);
        }

        protected Task<INavigationResult> Navigate<T>(Parameters parameters = null)
                                    where T : ViewModelBase
        {
            return navigationService.NavigateToViewModel<T>(parameters);
        }

        protected Task<INavigationResult> ReturnToRoot(Parameters parameters = null)
        {
            return navigationService.ReturnToRoot(parameters);
        }

        #endregion Navigation

        public virtual void Destroy()
        {
        }
    }
}