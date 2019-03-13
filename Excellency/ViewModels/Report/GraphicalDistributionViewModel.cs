using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class GraphicalDistributionViewModel
    {
        public IEnumerable<SelectListItem> Periods { get; set; }
    }
}
