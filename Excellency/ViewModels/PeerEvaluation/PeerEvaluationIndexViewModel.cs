using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class PeerEvaluationIndexViewModel
    {
        public IEnumerable<PeerEvaluationListingItem> Evaluations { get; set; }
        public IEnumerable<EmployeeItem> Employees { get; set; }
    }
}
