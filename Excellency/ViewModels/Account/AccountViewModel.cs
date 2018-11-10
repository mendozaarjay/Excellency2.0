using System.ComponentModel.DataAnnotations;

namespace Excellency.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50,ErrorMessage = "First name should be less than or equal to 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "Last name should be less than or equal to 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Mobile is required.")]
        [MaxLength(50, ErrorMessage = "Mobile should be less than or equal to 50 characters.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(255, ErrorMessage = "Email should be less than or equal to 255 characters.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email address.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username should be less than or equal to 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password did not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Company is required.")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Branch is required.")]
        public string Branch { get; set; }
        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Position is required.")]
        public string Position { get; set; }
    }
}
