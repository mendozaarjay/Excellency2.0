using System;
using System.ComponentModel.DataAnnotations;

namespace Excellency.Models
{
    public class Category
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        public int Weight { get; set; }

        public virtual KeySuccessIndicator SuccessIndicator { get; set; }

        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
