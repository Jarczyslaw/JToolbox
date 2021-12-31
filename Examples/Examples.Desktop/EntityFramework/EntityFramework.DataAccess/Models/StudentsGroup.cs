using JToolbox.DataAccess.EF.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFramework.DataAccess.Models
{
    public class StudentsGroup : BaseExtendedModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}