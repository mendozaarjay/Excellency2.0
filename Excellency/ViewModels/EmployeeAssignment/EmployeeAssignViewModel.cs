﻿using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class EmployeeAssignViewModel
    {
        public EmployeeViewModel Employee { get; set; }
        public IEnumerable<KeyResultAreaViewModel> KeyResultAreas { get; set; }
        public string[] SelectedKeyResultAreas { get; set; }
        public IEnumerable<BehavioralFactorViewModel> BehavioralFactors { get; set; }
        public string[] SelectedBehavioralFactors { get; set; }

        public IEnumerable<AssignedBehavioralViewModel> AssignedBehavioralItems { get; set; }
        public IEnumerable<AssignedKeyResultViewModel> AssignedKeyResultsItems { get; set; }

    }
}
