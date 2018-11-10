using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class EmployeeEvaluationViewModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public IEnumerable<EvaluationCategoryViewModel> BehavioralCategories { get; set; }
        public IEnumerable<EvaluationCategoryViewModel> KRACategories { get; set; }
    }
}
