using System.ComponentModel.DataAnnotations;

namespace Excellency.Models
{
    public class Status
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
