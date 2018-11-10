using System.ComponentModel.DataAnnotations;

namespace Excellency.ViewModels
{
    public class BehavioralItemViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        [Required(ErrorMessage = "Score is required.")]
        public int Score { get; set; }
        public string Comment { get; set; }
    }
}
