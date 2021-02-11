using Acr.UserDialogs;
using System;
using System.Threading.Tasks;

namespace JToolbox.XamarinForms.Dialogs
{
    public interface IDialogsService
    {
        IUserDialogs UserDialogs { get; }

        Task Busy(string message, Action busyAction);
        Task Busy(string message, Func<Task> busyAction);
        Task<T> Busy<T>(string message, Func<T> busyAction);
        Task<T> Busy<T>(string message, Func<Task<T>> busyAction);
        Task Error(Exception exc, string message);
        Task Error(string message);
        Task Information(string message);
        Task<bool> QuestionYesNo(string message, string title = "Question");
        Task<T> ShowActionSheet<T>(ActionSheet<T> actionSheet);
        Task ShowLoading(string message, Func<Task> loadingAction, Action cancelAction = null);
        void Toast(string message, TimeSpan? duration = null);
        Task Warning(string message);
    }
}