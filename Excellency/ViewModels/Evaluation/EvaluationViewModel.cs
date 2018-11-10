using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class EvaluationViewModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Branch { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public int KeyResultAreaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public IEnumerable<KeySuccessIndicatorViewModel> SuccessIndicators { get; set; }
        public IEnumerable<EvaluationRatingTableViewModel> RatingTableItems { get; set; }
    }
}
