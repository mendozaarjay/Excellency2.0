using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class EmployeeEvaluationViewModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public IEnumerable<EvaluationCategoryViewModel> BehavioralCategories { get; set; }
        public IEnumerable<EvaluationCategoryViewModel> KRACategories { get; set; }
        [NotMapped]
        public bool IsWithActiveSeason { get; set; }
    }
}
