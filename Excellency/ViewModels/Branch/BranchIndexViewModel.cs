using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class BranchIndexViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(255, ErrorMessage = "Description should be less than or equal to 255 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Company is required.")]
        public string CompanyId { get; set; }
        public string Company { get; set; }
        public IEnumerable<BranchViewModel> Branches { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }
    }
}
