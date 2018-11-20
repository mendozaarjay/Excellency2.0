using System.Collections.Generic;

namespace Excellency.Dashboard
{
    public class EmployeeDashboardViewModel
    {
        //Employee
        public int EmployeeId { get; set; }
        public IEnumerable<EvaluationCriteria> BehavioralStrength { get; set; }
        public IEnumerable<EvaluationCriteria> BehavioralWeakness { get; set; }
        public IEnumerable<EvaluationCriteria> KeyResultStrength { get; set; }
        public IEnumerable<EvaluationCriteria> KeyResultWeakness { get; set; }
        public string EmployeeName { get; set; }
        public int TotalScore { get; set; }
    }
}
