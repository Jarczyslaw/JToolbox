using JToolbox.Core.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace JToolbox.DataAccess.EF.Models
{
    public class BaseModel : IKey
    {
        [Key]
        public int Id { get; set; }
    }
}