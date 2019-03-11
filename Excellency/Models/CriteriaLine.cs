using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.Models
{
    public class CriteriaLine
    {
        public virtual CriteriaHeader CriteriaHeader { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Weight { get; set; }
        public bool IsDeleted { get; set; } = false;


    }
}
