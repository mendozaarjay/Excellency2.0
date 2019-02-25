using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class EvaluationSeasonIndexViewModel
    {
        public IEnumerable<EvaluationSeasonItem> Seasons { get; set; }
        public EvaluationSeasonItem Evaluation { get; set; }
    }
}
