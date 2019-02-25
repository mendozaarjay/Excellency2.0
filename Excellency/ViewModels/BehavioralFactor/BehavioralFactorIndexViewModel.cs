using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class BehavioralFactorIndexViewModel
    {
        public BehavioralFactorViewModel BehavioralFactor { get; set; }
        public IEnumerable<BehavioralFactorViewModel> BehavioralFactors { get; set; }
        public IEnumerable<SelectListItem> EmployeeCategories { get; set; }
        [NotMapped]
        public bool IsWithActiveSeason { get; set; }
        [NotMapped]
        public EvaluationSeasonItem ActiveSeason { get; set; }
    }
}
