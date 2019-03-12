using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class CreatePeerEvaluationViewModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public EvaluatePeerHeaderViewModel Header { get; set; }
        public List<EvaluatePeerLineViewModel> LineItems { get; set; }
    }
}
