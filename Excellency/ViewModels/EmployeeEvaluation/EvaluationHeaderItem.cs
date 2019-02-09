using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class EvaluationHeaderItem
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Rater { get; set; }
        public string DateRated { get; set; }
        public string Status { get; set; }
        public string Approver { get; set; }
        public string ApprovalDate { get; set; }
        public string ApproverRemarks { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
    }
}
