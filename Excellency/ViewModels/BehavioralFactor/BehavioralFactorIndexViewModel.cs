using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class BehavioralFactorIndexViewModel
    {
        public BehavioralFactorViewModel BehavioralFactor { get; set; }
        public IEnumerable<BehavioralFactorViewModel> BehavioralFactors { get; set; }
        public IEnumerable<SelectListItem> EmployeeCategories { get; set; }
    }
}
