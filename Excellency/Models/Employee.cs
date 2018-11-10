using System;
using System.ComponentModel.DataAnnotations;

namespace Excellency.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string EmployeeNo { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string MiddleName { get; set; }

        public virtual Company Company { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Department Department { get; set; }
        public virtual Position Position { get; set; }
        public virtual EmployeeCategory Category { get; set; }

        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
