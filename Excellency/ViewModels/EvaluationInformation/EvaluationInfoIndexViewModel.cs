using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class EvaluationInfoIndexViewModel
    {
        public string Name { get; set; }
        public IEnumerable<EvaluationBehavioralItem> BehavioralItems { get; set; }
        public IEnumerable<EvaluationKRAItem> KRAItems { get; set; }
        public EIApprovalLevel ApprovalLevel { get; set; }
    }
}
