using JToolbox.DataAccess.EF.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.DataAccess.Models
{
    public class Student : BaseExtendedModel
    {
        public virtual ICollection<Assessment> Assessments { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [ForeignKey(nameof(StudentsGroupId))]
        public virtual StudentsGroup StudentsGroup { get; set; }

        public int? StudentsGroupId { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}