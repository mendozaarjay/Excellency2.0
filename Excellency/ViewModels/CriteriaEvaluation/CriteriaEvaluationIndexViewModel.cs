using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class CriteriaEvaluationIndexViewModel
    {
        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}
