using System.ComponentModel.DataAnnotations;

namespace Excellency.Models
{
    public class BehavioralFactorItem
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public int Weight { get; set; }
        public virtual BehavioralFactor BehavioralFactor { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
