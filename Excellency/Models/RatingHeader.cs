using System;
using System.ComponentModel.DataAnnotations;

namespace Excellency.Models
{
    public class RatingHeader
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual Account Ratee { get; set; }
        public virtual Account Rater { get; set; }
        public DateTime PostedDate { get; set; }

        public virtual Account FirstApprover { get; set; }
        public string FirstApproverRemarks { get; set; }
        public DateTime FirstApprovalDate { get; set; }

        public virtual Account SecondApprover { get; set; }
        public string SecondApproverRemarks { get; set; }
        public DateTime SecondApprovalDate { get; set; }

        public virtual Status Status { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }


        public bool IsExpired { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
