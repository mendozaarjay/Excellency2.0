using System.ComponentModel.DataAnnotations;

namespace Excellency.ViewModels
{
    public class EditBehavioralItemViewModel
    {
        public int RecordId { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        [Required(ErrorMessage = "Score is required.")]
        public int Score { get; set; }
        public string Comment { get; set; }
    }
}
