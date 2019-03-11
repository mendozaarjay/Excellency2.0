using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class AppraisalHistoryIndexViewModel
    {
        public IEnumerable<SelectListItem> Periods { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}
