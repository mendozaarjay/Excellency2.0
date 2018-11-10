using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Models
{
    public class EmployeeRaterLine
    {
        public int Id { get; set; }
        public virtual EmployeeRaterHeader EmployeeRater { get; set; }
        public virtual Employee Employee { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
