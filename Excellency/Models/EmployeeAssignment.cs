using System;

namespace Excellency.Models
{
    public class EmployeeAssignment
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }

        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
