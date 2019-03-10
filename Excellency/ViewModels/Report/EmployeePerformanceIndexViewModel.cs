using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class EmployeePerformanceIndexViewModel
    {
        public IEnumerable<SelectListItem> Periods { get; set; }
    }
}
