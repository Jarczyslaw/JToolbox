using System.ComponentModel.DataAnnotations;

namespace JToolbox.DataAccess.EntityFrameworkAccess.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}