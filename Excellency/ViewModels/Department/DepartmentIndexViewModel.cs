using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class DepartmentIndexViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<DepartmentViewModel> Departments { get; set; }
    }
}
