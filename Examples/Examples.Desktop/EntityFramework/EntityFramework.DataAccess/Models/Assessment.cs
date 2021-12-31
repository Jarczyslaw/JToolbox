using JToolbox.DataAccess.EF.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.DataAccess.Models
{
    public class Assessment : BaseExtendedModel
    {
        public string Notes { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }

        public int StudentId { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public virtual Subject Subject { get; set; }

        public int SubjectId { get; set; }

        [Required]
        public int Value { get; set; }
    }
}