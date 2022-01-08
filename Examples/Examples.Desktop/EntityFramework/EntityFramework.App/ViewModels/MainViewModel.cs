using EntityFramework.DataAccess;
using JToolbox.Desktop.Dialogs;
using JToolbox.WPF.Core.Base;
using System.Linq;

namespace EntityFramework.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IDialogsService dialogsService)
        {
            Students = new StudentsViewModel(dialogsService);
            StudentGroups = new StudentGroupsViewModel(dialogsService);

            using (var ctx = new Context())
            {
                ctx.Students.ToList();
            }
        }

        public StudentGroupsViewModel StudentGroups { get; set; }
        public StudentsViewModel Students { get; set; }
    }
}