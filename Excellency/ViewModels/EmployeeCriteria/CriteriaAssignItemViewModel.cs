using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class CriteriaAssignItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }

        public IEnumerable<CriteriaAssignmentViewModel> Items { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}
