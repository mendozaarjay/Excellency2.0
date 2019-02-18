using System;

namespace Excellency.Models
{
    public class Recommendation
    {
        public int Id { get; set; }
        public Account Employee { get; set; }
        public string Comment { get; set; }
        public bool IsExpired { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
