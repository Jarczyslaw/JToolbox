using EntityFramework.App.ViewModels.Common;
using EntityFramework.BusinessLogic.Models;
using JToolbox.Desktop.Dialogs;
using System.Collections.Generic;

namespace EntityFramework.App.ViewModels
{
    public class StudentGroupsViewModel : CommonListViewModel<StudentGroup>
    {
        public StudentGroupsViewModel(IDialogsService dialogs) : base(dialogs)
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
            return new List<StudentGroup>()
            {
                new StudentGroup
                {
                    Id = 1,
                    Name = "Group1",
                },
                new StudentGroup
                {
                    Id = 2,
                    Name = "Group2",
                },
            };
        }

        protected override void RemoveItem(StudentGroup item)
        {
            throw new System.NotImplementedException();
        }
    }
}