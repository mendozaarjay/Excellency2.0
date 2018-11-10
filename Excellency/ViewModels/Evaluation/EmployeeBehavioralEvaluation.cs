using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class EmployeeBehavioralEvaluation
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int BehavioralId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public List<BehavioralItemViewModel> BehavioralItems { get; set; }
    }
}
