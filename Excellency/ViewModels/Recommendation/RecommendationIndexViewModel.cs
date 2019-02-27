using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class RecommendationIndexViewModel
    {
        public IEnumerable<RecommendationIndexItem> Accounts { get; set; }
        [NotMapped]
        public bool IsWithActiveSeason { get; set; }
        [NotMapped]
        public EvaluationSeasonItem ActiveSeason { get; set; }
    }
}
