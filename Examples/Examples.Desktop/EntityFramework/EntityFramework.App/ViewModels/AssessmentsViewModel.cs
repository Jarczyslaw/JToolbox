using EntityFramework.App.ViewModels.Common;
using EntityFramework.BusinessLogic;
using EntityFramework.BusinessLogic.Models;
using JToolbox.Desktop.Dialogs;
using System.Collections.Generic;

namespace EntityFramework.App.ViewModels
{
    public class AssessmentsViewModel : CommonListViewModel<Assessment>
    {
        public AssessmentsViewModel(IBusinessService businessService, IDialogsService dialogsService)
            : base(businessService, dialogsService)
        {
        }

        protected override void AddItem()
        {
        }

        protected override void EditItem(Assessment item)
        {
        }

        protected override IEnumerable<Assessment> GetItems()
        {
            return business.GetAssessments();
        }

        protected override void RemoveItem(Assessment item)
        {
        }
    }
}