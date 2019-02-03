using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Models
{
    public class EmployeeEvaluation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Score { get; set; }
        public string Status { get; set; }
    }
}
