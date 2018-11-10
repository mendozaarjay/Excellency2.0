using System.ComponentModel.DataAnnotations;

namespace Excellency.Models
{
    public class RatingTableItem
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        public int Weight { get; set; }
        public bool IsDeleted { get; set; }
        public virtual RatingTable RatingTable { get; set; }
    }
}
