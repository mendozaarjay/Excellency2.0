using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class AccountRegisterViewModel
    {
        [NotMapped]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee number is required.")]
        [MaxLength(50, ErrorMessage = "Employee number should be less than or equal to 50 characters.")]
        public string EmployeeNo { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "First name should be less than or equal to 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "Last name should be less than or equal to 50 characters.")]
        public string LastName { get; set; }


        [NotMapped]
        public string MiddleName { get; set; }

        public string Mobile { get; set; }
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
        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        public IEnumerable<SelectListItem> Companies { get; set; }
        public IEnumerable<SelectListItem> Branches { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
        public IEnumerable<SelectListItem> Positions { get; set; }   
        public IEnumerable<SelectListItem> Categories { get; set; }

    }
}
