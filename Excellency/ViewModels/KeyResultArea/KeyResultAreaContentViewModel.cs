using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class KeyResultAreaContentViewModel
    {
        public int KeyResultAreaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<KeySuccessIndicatorViewModel> SuccessIndicators { get; set; }

        public int KRAId { get; set; }
        public int KSIId { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(255,ErrorMessage = "Description should be less than or equal to 255 characters.")]
        public string KSIDescription { get; set; }
        public IEnumerable<RatingTableViewModel> RatingTables { get; set; }
        public int KSIWeight { get; set; }
        public string KSITitle { get; set; }

    }
}
