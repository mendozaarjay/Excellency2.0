using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class EditBehavioralEvaluation
    {
        public int HeaderId { get; set; }
        public int FactorId { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int BehavioralId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public List<EditBehavioralItemViewModel> BehavioralItems { get; set; }
    }
}
