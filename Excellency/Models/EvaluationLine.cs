using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.Models
{
    public class EvaluationLine
    {
        public int Id { get; set; }
        public virtual EvaluationHeader EvaluationHeader { get; set; }
        public virtual KeySuccessIndicator SuccessIndicator { get; set; }
        public virtual RatingTableItem RatingTableItem { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Score { get; set; }
        public string Comment { get; set; }
    }
}
