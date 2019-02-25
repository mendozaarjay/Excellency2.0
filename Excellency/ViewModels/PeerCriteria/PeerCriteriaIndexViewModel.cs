using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class PeerCriteriaIndexViewModel
    {
        public PeerCriteriaHeaderViewModel PeerCriteria { get; set; }
        public IEnumerable<PeerCriteriaHeaderViewModel> Criterias { get; set; }
        [NotMapped]
        public bool IsWithActiveSeason { get; set; }
        [NotMapped]
        public EvaluationSeasonItem ActiveSeason { get; set; }
    }
}
