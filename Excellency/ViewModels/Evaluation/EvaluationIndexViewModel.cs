using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class EvaluationIndexViewModel
    {
        public IEnumerable<EvaluationEmployeeViewModel> Employees { get; set; }
        public IEnumerable<KeyResultAreaViewModel> KeyResultAreas { get; set; }

    }
}
