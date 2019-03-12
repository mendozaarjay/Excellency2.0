namespace Excellency.ViewModels
{
    public class CriteriaEvaluationOverViewModel
    {
        public int RecordId { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public bool IsEvaluated { get; set; }
        public bool IsPosted { get; set; }
    }
}
