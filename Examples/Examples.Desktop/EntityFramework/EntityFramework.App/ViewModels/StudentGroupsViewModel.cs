using EntityFramework.App.ViewModels.Common;
using EntityFramework.BusinessLogic;
using EntityFramework.BusinessLogic.Models;
using JToolbox.Desktop.Dialogs;
using System.Collections.Generic;

namespace EntityFramework.App.ViewModels
{
    public class StudentGroupsViewModel : CommonListViewModel<StudentGroup>
    {
        public StudentGroupsViewModel(IBusinessService businessService, IDialogsService dialogsService)
            : base(businessService, dialogsService)
        {
        }

        protected override void AddItem()
        {
            throw new System.NotImplementedException();
        }

        protected override void EditItem(StudentGroup item)
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerable<StudentGroup> GetItems()
        {
            return business.GetStudentGroups();
        }

        protected override void RemoveItem(StudentGroup item)
        {
            throw new System.NotImplementedException();
        }
    }
}