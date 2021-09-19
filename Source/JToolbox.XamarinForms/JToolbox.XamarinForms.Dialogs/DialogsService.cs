using Acr.UserDialogs;
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
            return UserDialogs.AlertAsync(message, "Error", "OK");
        }

        public Task Error(Exception exc, string message)
        {
            var msg = exc.Message;
            if (!string.IsNullOrEmpty(message))
            {
                msg = message + msg;
            }
            return UserDialogs.AlertAsync(msg, "Error", "OK");
        }

        public Task Information(string message)
        {
            return UserDialogs.AlertAsync(message, "Information", "OK");
        }

        public Task<bool> QuestionYesNo(string message, string title = "Question")
        {
            return UserDialogs.ConfirmAsync(message, title, "Yes", "No");
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

            var dialog = UserDialogs.Loading(message, () => cancelAction?.Invoke(), "Cancel", maskType: MaskType.Gradient);
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
            return UserDialogs.AlertAsync(message, "Warning", "OK");
        }
    }
}