using Excellency.ViewModels;
using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class ApprovalBehavioralViewModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string RaterName { get; set; }
        public string BehavioralFactor { get; set; }
        public string Weight { get; set; }
        public string TotalScore { get; set; }
        public string Remarks { get; set; }
        public string Action { get; set; }
        public IEnumerable<ApprovalBehavioralItemViewModel> Items { get; set; }
    }
}
