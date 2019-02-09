using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class PeerEvaluationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PeerEvaluationHeaderViewModel Header { get; set; }
        public List<PeerEvaluationLineItemViewModel> LineItems { get; set; }
    }
}
