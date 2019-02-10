using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class ApprovalLevelAssignViewModel
    {
        [NotMapped]
        public int EmployeeId { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public ApprovalLevelItemViewModel ApprovalLevel { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> FirstApprovers { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> SecondApprovers { get; set; }
    }
}
