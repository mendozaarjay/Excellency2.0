using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Models
{
    public class EvaluationResultItem
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public string Percentage { get; set; }
        public decimal Score { get; set; }
        public decimal Total { get; set; }
    }
}
