using System;

namespace Excellency.Models
{
    public class ApprovalLevelAssignment
    {
        public int Id { get; set; }
        public virtual Account Employee { get; set; }
        public virtual Account FirstApproval { get; set; }
        public virtual Account SecondApproval { get; set; }

        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }


    }
}
