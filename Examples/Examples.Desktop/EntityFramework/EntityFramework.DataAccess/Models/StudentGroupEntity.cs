using JToolbox.DataAccess.EF.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.DataAccess.Models
{
    [Table("StudentGroups")]
    public class StudentGroupEntity : BaseExtendedModel
    {
        public virtual StudentEntity Leader { get; set; }

        public int? LeaderId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<StudentEntity> Students { get; set; }
    }
}