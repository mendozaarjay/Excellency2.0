using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class EmployeeRegisterViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Employee number is required.")]
        [MaxLength(10)]
        public string EmployeeNo { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Middlename is required.")]
        [MaxLength(50)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Company is required.")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Branch is required.")]
        public string Branch { get; set; }
        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Position is required.")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Employee Category is required.")]
        public string Category { get; set; }

        public IEnumerable<SelectListItem> Companies { get; set; }
        public IEnumerable<SelectListItem> Branches { get; set; }
        public IEnumerable<SelectListItem> Positions { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
        public IEnumerable<SelectListItem> EmployeeCategories { get; set; }
    }
}
