using EntityFramework.BusinessLogic.Models.Common;
using System.Collections.Generic;

namespace EntityFramework.BusinessLogic.Models
{
    public class Subject : BaseExtendedModel
    {
        public List<Assessment> Assessments { get; set; }

        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}