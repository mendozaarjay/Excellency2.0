using System.ComponentModel.DataAnnotations;

namespace Excellency.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(255,ErrorMessage = "Description should be less than or equal to 255 characters.")]
        public string Description { get; set; }
    }
}
