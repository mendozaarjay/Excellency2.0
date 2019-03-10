using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class RecommendationAssignViewModel
    {
        public int Id { get; set; }
        public IEnumerable<RecommendationAssignmentViewModel> Assigned { get; set; }
        public IEnumerable<RecommendationAccountItemViewModel> Employees { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
    }
}
