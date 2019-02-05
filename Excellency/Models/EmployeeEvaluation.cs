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
        public decimal TotalScore { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal ConvertedScore { get; set; }
        public decimal WeightedScore { get; set; }
        public decimal Percentage { get; set; }

    }
}
