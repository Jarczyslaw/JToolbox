using System.ComponentModel.DataAnnotations;

namespace JToolbox.DataAccess.EF.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}