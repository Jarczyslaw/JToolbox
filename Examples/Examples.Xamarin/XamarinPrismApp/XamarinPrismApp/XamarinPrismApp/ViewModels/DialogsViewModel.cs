using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using XamarinPrismApp.Models;

namespace XamarinPrismApp.ViewModels
{
    public class DialogsViewModel : ViewModelBase
    {
        private readonly IDialogsService dialogsService;

        public DialogsViewModel(IDialogsService dialogsService, INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Dialogs";
            this.dialogsService = dialogsService;
            InitializeEntries();
        }

        public ObservableCollection<DialogsEntryViewModel> Entries { get; set; }
        public DialogsEntryViewModel SelectedEntry { get; set; }

        public DelegateCommand ShowCommand => new DelegateCommand(async () =>
        {
            if (SelectedEntry == null)
            {
                return;
            }

            switch (SelectedEntry.Type)
            {
                case DialogsEntryType.Alert:
                    await ShowAlert();
                    break;

                case DialogsEntryType.Toast:
                    ShowToast();
                    break;

                case DialogsEntryType.Confirm:
                    await ShowConfirm();
                    break;

                case DialogsEntryType.DatePrompt:
                    await ShowDatePrompt();
                    break;

                case DialogsEntryType.Loading:
                    ShowLoading();
                    break;

                case DialogsEntryType.Prompt:
                    await ShowPrompt();
                    break;

                case DialogsEntryType.Busy:
                    ShowBusy();
                    break;

                case DialogsEntryType.ActionSheet:
                    await ShowActionSheet();
                    break;

                default:
                    break;
            }
        });

        private async Task ShowActionSheet()
        {
            var actionSheet = new ActionSheet<int>()
            {
                Message = "Message",
                Title = "Title"
            };
            actionSheet.AddOption("Option1", 1);
            actionSheet.AddOption("Option2", 2);
            actionSheet.AddOption("Option3", 3);
            actionSheet.AddCancel("Cancel");
            var result = await dialogsService.ShowActionSheet(actionSheet);
            if (result != default)
            {
                dialogsService.Toast(result.ToString());
            }
        }

        private void ShowBusy()
        {
            dialogsService.Busy(null, async () => await Task.Delay(3000));
        }

        private async Task ShowPrompt()
        {
            var result = await dialogsService.UserDialogs.PromptAsync("Message", "Title", "OK", "Cancel", "Placeholder", Acr.UserDialogs.InputType.Name);
            if (result.Ok)
            {
                dialogsService.Toast("Inserted text: " + result.Text);
            }
        }

        private void ShowLoading()
        {
            dialogsService.ShowLoading("Title", async () =>
            {
                await Task.Delay(5000);
                dialogsService.Toast("Finished");
            }, () => dialogsService.Toast("Cancelled"));
        }

        private async Task ShowDatePrompt()
        {
            var result = await dialogsService.UserDialogs.DatePromptAsync("Title", DateTime.Now);
            if (result.Ok)
            {
                dialogsService.Toast("Selected date: " + result.SelectedDate.ToString("yyyy-MM-dd"));
            }
        }

        private async Task ShowConfirm()
        {
            var result = await dialogsService.UserDialogs.ConfirmAsync("Message", "Title", "OK", "Cancel");
            if (result)
            {
                dialogsService.Toast("Confirmed");
            }
        }

        private void ShowToast()
        {
            dialogsService.UserDialogs.Toast("Toast", TimeSpan.FromSeconds(5));
        }

        private Task ShowAlert()
        {
            return dialogsService.UserDialogs.AlertAsync("Message", "Title", "OK");
        }

        private void InitializeEntries()
        {
            var entries = new List<DialogsEntryViewModel>
            {
                new DialogsEntryViewModel(DialogsEntryType.Alert),
                new DialogsEntryViewModel(DialogsEntryType.Toast),
                new DialogsEntryViewModel(DialogsEntryType.Confirm),
                new DialogsEntryViewModel(DialogsEntryType.DatePrompt),
                new DialogsEntryViewModel(DialogsEntryType.Loading),
                new DialogsEntryViewModel(DialogsEntryType.Prompt),
                new DialogsEntryViewModel(DialogsEntryType.Busy),
                new DialogsEntryViewModel(DialogsEntryType.ActionSheet)
            };
            Entries = new ObservableCollection<DialogsEntryViewModel>(entries.OrderBy(p => p.Title)
                .ToList());
            SelectedEntry = Entries.First();
        }
    }
}