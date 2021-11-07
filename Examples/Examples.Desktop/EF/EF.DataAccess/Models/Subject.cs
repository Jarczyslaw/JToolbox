using JToolbox.DataAccess.EF.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EF.DataAccess.Models
{
    public class Subject : BaseExtendedModel
    {
        public virtual ICollection<Assessment> Assessments { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}