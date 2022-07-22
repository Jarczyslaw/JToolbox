using Acr.UserDialogs;
using JToolbox.XamarinForms.Dialogs.Resources;
using System;
using System.Threading.Tasks;

namespace JToolbox.XamarinForms.Dialogs
{
    public class DialogsService : IDialogsService
    {
        public DialogsService(IUserDialogs userDialogs)
        {
            UserDialogs = userDialogs;
        }

        public IUserDialogs UserDialogs { get; }

        public async Task Busy(string message, Action busyAction)
        {
            using (new BusyIndicator(message))
            {
                await Task.Run(busyAction);
            }
        }

        public async Task<T> Busy<T>(string message, Func<T> busyAction)
        {
            using (new BusyIndicator(message))
            {
                return await Task.Run(() => busyAction());
            }
        }

        public async Task Busy(string message, Func<Task> busyAction)
        {
            using (new BusyIndicator(message))
            {
                await busyAction();
            }
        }

        public async Task<T> Busy<T>(string message, Func<Task<T>> busyAction)
        {
            using (new BusyIndicator(message))
            {
                return await busyAction();
            }
        }

        public Task Error(string message)
        {
            return UserDialogs.AlertAsync(message, Translations.Error, Translations.Ok);
        }

        public Task Error(Exception exc, string message)
        {
            var msg = exc.Message;
            if (!string.IsNullOrEmpty(message))
            {
                msg = message + msg;
            }
            return UserDialogs.AlertAsync(msg, Translations.Error, Translations.Ok);
        }

        public Task Information(string message)
        {
            return UserDialogs.AlertAsync(message, Translations.Information, Translations.Ok);
        }

        public Task<bool> QuestionYesNo(string message, string title = null)
        {
            title = title ?? Translations.Question;
            return UserDialogs.ConfirmAsync(message, title, Translations.Yes, Translations.No);
        }

        public Task<T> ShowActionSheet<T>(ActionSheet<T> actionSheet)
        {
            return Task.Run(() =>
            {
                var t = new TaskCompletionSource<T>();
                actionSheet.ActionSheetSelected += v => t.TrySetResult(v);
                UserDialogs.ActionSheet(actionSheet);
                return t.Task;
            });
        }

        public async Task ShowLoading(string message, Func<Task> loadingAction, Action cancelAction = null)
        {
            if (!string.IsNullOrEmpty(message))
            {
                message += Environment.NewLine;
            }

            var dialog = UserDialogs.Loading(message, () => cancelAction?.Invoke(), Translations.Cancel, maskType: MaskType.Gradient);
            try
            {
                await loadingAction();
            }
            finally
            {
                dialog.Hide();
            }
        }

        public void Toast(string message, TimeSpan? duration = null)
        {
            UserDialogs.Toast(message, duration);
        }

        public Task Warning(string message)
        {
            return UserDialogs.AlertAsync(message, Translations.Warning, Translations.Ok);
        }
    }
}