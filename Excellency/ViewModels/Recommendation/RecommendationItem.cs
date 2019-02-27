using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class RecommendationItem
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Recommendation { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        [NotMapped]
        public bool IsWithActiveSeason { get; set; }
        [NotMapped]
        public EvaluationSeasonItem ActiveSeason { get; set; }
    }
}
