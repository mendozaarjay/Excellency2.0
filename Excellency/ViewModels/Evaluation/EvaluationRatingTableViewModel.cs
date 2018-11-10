using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class EvaluationRatingTableViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public int KSIId { get; set; }
    }
}
