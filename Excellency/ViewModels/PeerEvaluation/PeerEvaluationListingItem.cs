namespace Excellency.ViewModels
{
    public class PeerEvaluationListingItem
    {
        public int EvaluationId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string EvaluationDate { get; set; }
        public string Status { get; set; }
        public int TotalScore { get; set; }
    }
}
