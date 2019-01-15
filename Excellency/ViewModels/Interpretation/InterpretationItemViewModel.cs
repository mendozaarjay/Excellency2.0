using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class InterpretationItemViewModel
    {
        [NotMapped]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Score From is required.")]
        public decimal ScoreFrom { get; set; }
        [Required(ErrorMessage = "Score To is required.")]
        public decimal ScoreTo { get; set; }
    }
}
