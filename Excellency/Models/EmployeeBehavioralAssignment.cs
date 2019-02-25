namespace Excellency.Models
{
    public class EmployeeBehavioralAssignment
    {
        public int Id { get; set; }
        public virtual EmployeeAssignment EmployeeAssignment { get; set; }
        public BehavioralFactor BehavioralFactor { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual EvaluationSeason EvaluationSeason { get; set; }
    }
}
