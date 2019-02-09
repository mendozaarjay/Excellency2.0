using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class EvaluationLineItem
    {
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public decimal Score { get; set; }
        public string Comment { get; set; }
    }
}
