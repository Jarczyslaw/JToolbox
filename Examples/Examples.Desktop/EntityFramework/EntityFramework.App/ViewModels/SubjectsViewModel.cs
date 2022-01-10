using EntityFramework.App.ViewModels.Common;
using EntityFramework.BusinessLogic;
using EntityFramework.BusinessLogic.Models;
using JToolbox.Desktop.Dialogs;
using System.Collections.Generic;

namespace EntityFramework.App.ViewModels
{
    public class SubjectsViewModel : CommonListViewModel<Subject>
    {
        public SubjectsViewModel(IBusinessService businessService, IDialogsService dialogsService)
            : base(businessService, dialogsService)
        {
        }

        protected override void AddItem()
        {
        }

        protected override void EditItem(Subject item)
        {
        }

        protected override IEnumerable<Subject> GetItems()
        {
            return business.GetSubjects();
        }

        protected override void RemoveItem(Subject item)
        {
        }
    }
}