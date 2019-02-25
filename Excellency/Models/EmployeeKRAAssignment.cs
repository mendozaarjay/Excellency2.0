namespace Excellency.Models
{
    public class EmployeeKRAAssignment
    {
        public int Id { get; set; }
        public virtual EmployeeAssignment EmployeeAssignment { get; set; }
        public KeyResultArea KeyResultArea { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual EvaluationSeason EvaluationSeason { get; set; }
    }
}
