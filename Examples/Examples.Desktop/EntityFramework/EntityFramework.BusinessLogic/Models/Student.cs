using EntityFramework.BusinessLogic.Models.Common;
using System.Collections.Generic;

namespace EntityFramework.BusinessLogic.Models
{
    public class Student : BaseExtendedModel
    {
        public List<Assessment> Assessments { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public StudentGroup StudentsGroup { get; set; }

        public int? StudentsGroupId { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}