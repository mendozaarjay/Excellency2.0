using System;

namespace Excellency.Models
{
    public class CriteriaEvaluationHeader
    {
        public int Id { get; set; }
        public Account Rater { get; set; }
        public Account Ratee { get; set; }
        public CriteriaHeader Criteria { get; set; }
        public DateTime DateCreated { get; set; }
        public Status Status { get; set; }
        public EvaluationSeason Period { get; set; }
    }
}
