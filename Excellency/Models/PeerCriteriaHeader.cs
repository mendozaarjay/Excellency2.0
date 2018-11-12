using System;

namespace Excellency.Models
{
    public class PeerCriteriaHeader
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }


        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
