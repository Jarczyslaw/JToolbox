using EntityFramework.App.ViewModels.Common;
using EntityFramework.BusinessLogic.Models;
using JToolbox.Desktop.Dialogs;
using System.Collections.Generic;

namespace EntityFramework.App.ViewModels
{
    public class StudentsViewModel : CommonListViewModel<Student>
    {
        public StudentsViewModel(IDialogsService dialogsService) : base(dialogsService)
        {
        }

        protected override void AddItem()
        {
        }

        protected override void EditItem(Student item)
        {
        }

        protected override IEnumerable<Student> GetItems()
        {
            return new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    FirstName = "John"
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Mark"
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Denis"
                },
            };
        }

        protected override void RemoveItem(Student item)
        {
        }
    }
}