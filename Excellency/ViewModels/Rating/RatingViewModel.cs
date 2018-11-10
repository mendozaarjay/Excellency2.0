using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class RatingViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(500, ErrorMessage = "Description should be less than or equal to 500 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Rating Score is required.")]
        public int Score { get; set; }
    }
}
