using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.Models
{
    public class Interpretation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ScoreFrom { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ScoreTo { get; set; }

        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
