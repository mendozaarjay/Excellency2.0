using System;

namespace Excellency.Models
{
    public class ApproverAssignment
    {
        public int Id { get; set; }
        public Account User { get; set; }
        public Account Approver { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
