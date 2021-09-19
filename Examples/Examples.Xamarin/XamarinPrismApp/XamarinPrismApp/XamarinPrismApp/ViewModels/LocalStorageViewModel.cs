using Acr.UserDialogs;
using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XamarinPrismApp.DataAccess;
using XamarinPrismApp.Models;

namespace XamarinPrismApp.ViewModels
{
    public class LocalStorageViewModel : ViewModelBase
    {
        private readonly IDataAccessService dataAccessService;
        private readonly IDialogsService dialogsService;
        private ObservableCollection<UserViewModel> entries;
        private UserViewModel selectedEntry;

        public LocalStorageViewModel(IDialogsService dialogsService, IDataAccessService dataAccessService, INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Local storage";

            this.dialogsService = dialogsService;
            this.dataAccessService = dataAccessService;

            UpdateUsers();
        }

        public DelegateCommand AddCommand => new DelegateCommand(async () =>
        {
            var name = await GetName();
            if (!string.IsNullOrEmpty(name))
            {
                dataAccessService.AddUser(new User
                {
                    Name = name,
                    UpdateDate = DateTime.Now
                });
                UpdateUsers();
            }
        });

        public DelegateCommand<UserViewModel> DeleteCommand => new DelegateCommand<UserViewModel>(async (parameter) =>
        {
            if (parameter != null)
            {
                var confirm = await dialogsService.QuestionYesNo($"Do you really want to delete {parameter.Name}?");
                if (confirm)
                {
                    dataAccessService.DeleteUser(parameter.Id);
                    UpdateUsers();
                }
            }
        });

        public DelegateCommand<UserViewModel> EditCommand => new DelegateCommand<UserViewModel>(async (parameter) =>
                {
                    if (parameter != null)
                    {
                        var name = await GetName(parameter.Name);
                        if (!string.IsNullOrEmpty(name))
                        {
                            dataAccessService.UpdateUser(parameter.Id, new User
                            {
                                Name = name,
                                UpdateDate = DateTime.Now
                            });
                            UpdateUsers();
                        }
                    }
                });

        public ObservableCollection<UserViewModel> Entries
        {
            get => entries;
            set => SetProperty(ref entries, value);
        }

        public UserViewModel SelectedEntry
        {
            get => selectedEntry;
            set => SetProperty(ref selectedEntry, value);
        }

        private async Task<string> GetName(string name = "")
        {
            var result = await dialogsService.UserDialogs.PromptAsync(new PromptConfig
            {
                Message = "Insert name",
                Title = "Insert name",
                Text = name
            });
            return result.Ok ? result.Text : null;
        }

        private void UpdateUsers()
        {
            Entries = new ObservableCollection<UserViewModel>(dataAccessService.GetUsers()
                .Select(u => new UserViewModel(u)));
        }
    }
}