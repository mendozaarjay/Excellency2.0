namespace Excellency.ViewModels
{
    public class EIEvaluationItem
    {
        public int RecordId { get; set; }
        public int Id { get; set; }
        public string EvaluatedBy { get; set; }
        public string EvaluationDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public string Status { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
