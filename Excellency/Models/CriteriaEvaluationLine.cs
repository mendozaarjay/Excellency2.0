using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.Models
{
    public class CriteriaEvaluationLine
    {
        public int Id { get; set; }
        public CriteriaEvaluationHeader Header { get; set; }
        public CriteriaLine CriteriaLine { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Score { get; set; }
        public string Comment { get; set; }
    }
}
