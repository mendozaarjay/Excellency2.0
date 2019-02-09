namespace Excellency.ViewModels
{
    public class PeerEvaluationHeaderViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string DateCreated { get; set; }
        public bool IsSaved { get; set; }
    }
}
