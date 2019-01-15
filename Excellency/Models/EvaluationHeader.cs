using System;
using System.ComponentModel.DataAnnotations;

namespace Excellency.Models
{
    public class EvaluationHeader
    {
        public int Id { get; set; }
        public virtual Account Ratee { get; set; }
        public virtual KeyResultArea  KeyResultArea { get; set; }

        public virtual Account Rater { get; set; }
        public DateTime PostedDate { get; set; }
        public virtual Account Approver { get; set; }
        public DateTime ApprovedDate { get; set; }

        public virtual Status Status { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }

        public string ApproverRemarks { get; set; }

        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
