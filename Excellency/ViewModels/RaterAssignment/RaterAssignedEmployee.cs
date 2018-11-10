using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class RaterAssignedEmployee
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public string EmployeeName { get; set; }
        public string Company { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }

    }
}
