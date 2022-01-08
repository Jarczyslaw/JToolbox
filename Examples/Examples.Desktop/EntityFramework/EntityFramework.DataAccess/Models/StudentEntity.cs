using JToolbox.DataAccess.EF.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.DataAccess.Models
{
    [Table("Students")]
    public class StudentEntity : BaseExtendedModel
    {
        public virtual ICollection<AssessmentEntity> Assessments { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [ForeignKey(nameof(StudentsGroupId))]
        public virtual StudentGroupEntity StudentsGroup { get; set; }

        public int? StudentsGroupId { get; set; }
        public virtual ICollection<SubjectEntity> Subjects { get; set; }
    }
}