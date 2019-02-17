using System;

namespace Excellency.Models
{
    public class PeerAssignment
    {
        public int Id { get; set; }
        public virtual Account Ratee { get; set; }
        public virtual Account Rater { get; set; }
        public bool IsExpired { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
