using EntityFramework.App.ViewModels.Common;
using EntityFramework.BusinessLogic;
using JToolbox.Desktop.Dialogs;
using JToolbox.WPF.Core.Base;
using System.Windows.Controls;

namespace EntityFramework.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private TabItem selectedTab;

        public MainViewModel(IBusinessService businessService, IDialogsService dialogsService)
        {
            Students = new StudentsViewModel(businessService, dialogsService);
            StudentGroups = new StudentGroupsViewModel(businessService, dialogsService);
            Subjects = new SubjectsViewModel(businessService, dialogsService);
            Assessments = new AssessmentsViewModel(businessService, dialogsService);
        }

        public AssessmentsViewModel Assessments { get; set; }

        public TabItem SelectedTab
        {
            get => selectedTab;
            set
            {
                Set(ref selectedTab, value);
                ((selectedTab.Content as UserControl)?.DataContext as IOnRefresh)?.OnRefresh();
            }
        }

        public StudentGroupsViewModel StudentGroups { get; set; }
        public StudentsViewModel Students { get; set; }
        public SubjectsViewModel Subjects { get; set; }
    }
}