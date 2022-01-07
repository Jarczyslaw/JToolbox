using JToolbox.Desktop.Dialogs;
using JToolbox.WPF.Core.Base;

namespace EntityFramework.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IDialogsService dialogsService)
        {
            Students = new StudentsViewModel(dialogsService);
        }

        public StudentsViewModel Students { get; set; }
    }
}