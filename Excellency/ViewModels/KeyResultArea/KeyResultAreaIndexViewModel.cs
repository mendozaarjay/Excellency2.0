using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class KeyResultAreaIndexViewModel
    {
        public IEnumerable<KeyResultAreaViewModel> KeyResultAreas { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(255,ErrorMessage = "Title should be less than or equal to 255 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(255, ErrorMessage = "Title should be less than or equal to 255 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Weight is required.")]
        public int Weight { get; set; }
        [NotMapped]
        public bool IsWithActiveSeason { get; set; }
        [NotMapped]
        public EvaluationSeasonItem ActiveSeason { get; set; }
    }
}
