using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class CategoryIndexViewModel
    {
        public int KSIId { get; set; }
        public string KSIDescription { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }


        public int Id { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(255, ErrorMessage = "Description should be less than or equal to 255 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Weight is required.")]
        public int Weight { get; set; }
    }
}
