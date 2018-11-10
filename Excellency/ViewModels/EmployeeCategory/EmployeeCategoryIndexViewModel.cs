using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class EmployeeCategoryIndexViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IEnumerable<EmployeeCategoryViewModel> EmployeeCategories { get; set; }
    }
}
