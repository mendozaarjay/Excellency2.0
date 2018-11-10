using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class RatingTableItemViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(255, ErrorMessage = "Description should be less than or equal to 255 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Weight is required.")]
        public int Weight { get; set; }

        public int RatingTableId { get; set; }
    }
}
