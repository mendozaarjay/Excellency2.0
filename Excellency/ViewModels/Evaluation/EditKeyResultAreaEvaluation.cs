using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class EditKeyResultAreaEvaluation
    {
        public int HeaderId { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int KeyResultAreaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public List<EditSuccessIndicatorItem> EvaluationSuccessIndicators { get; set; }
    }
}
