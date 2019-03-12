using System;

namespace Excellency.Models
{
    public class EmployeeCriteriaAssignment
    {
        public int Id { get; set; }
        public Account Employee { get; set; }

        public CriteriaHeader Criteria { get; set; }
        public EvaluationSeason Period { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
