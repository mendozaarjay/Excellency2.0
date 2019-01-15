using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public virtual Account Employee { get; set; }
        public virtual Account Account { get; set; }
        public bool IsUser { get; set; } = false;

        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
