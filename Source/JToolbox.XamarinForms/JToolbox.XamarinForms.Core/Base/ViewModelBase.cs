using JToolbox.XamarinForms.Core.Navigation;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;

namespace JToolbox.XamarinForms.Core.Base
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
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

        #region Navigation

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        protected Task Close()
        {
            return navigationService.Close();
        }

        protected Task Close<TArg, TResult>(NavigationParams<TArg, TResult> parameters)
        {
            return navigationService.Close(parameters);
        }

        protected Task Navigate<TViewModel>()
            where TViewModel : ViewModelBase
        {
            return navigationService.NavigateToViewModel<TViewModel>();
        }

        protected Task Navigate<TViewModel, TArg, TResult>(NavigationParams<TArg, TResult> parameters)
            where TViewModel : ViewModelBase
        {
            return navigationService.NavigateToViewModel<TViewModel, TArg, TResult>(parameters);
        }

        protected Task ReturnToRoot()
        {
            return navigationService.ReturnToRoot();
        }

        protected Task ReturnToRoot<TArg, TResult>(NavigationParams<TArg, TResult> parameters)
        {
            return navigationService.ReturnToRoot(parameters);
        }

        #endregion Navigation

        public virtual void Destroy()
        {
        }
    }
}