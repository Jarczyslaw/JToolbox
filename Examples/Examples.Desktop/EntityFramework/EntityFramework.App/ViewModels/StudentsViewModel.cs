﻿using EntityFramework.App.ViewModels.Common;
using EntityFramework.BusinessLogic;
using EntityFramework.BusinessLogic.Models;
using JToolbox.Desktop.Dialogs;
using System.Collections.Generic;

namespace EntityFramework.App.ViewModels
{
    public class StudentsViewModel : CommonListViewModel<Student>
    {
        public StudentsViewModel(IBusinessService businessService, IDialogsService dialogsService)
            : base(businessService, dialogsService)
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
            return business.GetStudents();
        }

        protected override void RemoveItem(Student item)
        {
        }
    }
}